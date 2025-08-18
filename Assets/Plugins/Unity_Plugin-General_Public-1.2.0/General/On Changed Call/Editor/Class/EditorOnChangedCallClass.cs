using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OnChangedCallClass<>),true)]
public class EditorOnChangedCallClass : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property, label, true);

        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
            
            object parentObject = null;
            object targetObject = null;
            
            GetTargetAndParentObject(property.serializedObject.targetObject, property.propertyPath, out parentObject, out targetObject);
            
            UseClassMethod(targetObject);
            
            
            if (parentObject.GetType().IsArray == false  && parentObject is IEnumerable == false )
            {
                UseOutMethod(property, parentObject);
            }
        }
        
    }
    
    
    private void UseClassMethod(object property )
    {
        AbstOnValuCall absMethod = property as AbstOnValuCall;
        
        absMethod.OnValueChange();
    }


    private void UseOutMethod(SerializedProperty property,object parent)
    {
        string methodName = property.FindPropertyRelative("_methodName").stringValue;
        
        if (methodName != string.Empty)
        {
            var types = parent.GetType();
            MethodInfo method = null;
           
            foreach (var VARIABLE in types.GetMethods())
            {
                if (VARIABLE.Name == methodName)
                {
                    method = VARIABLE;
                    break;
                }
            }

            if (method != null && method.GetParameters().Count() == 0)
            {
                method.Invoke(parent, null);
            }
        }
    }

    
    
    private void GetTargetAndParentObject(object objectBeginningPath, string targetPathc,out object parentObject, out object targetObject)
    {
        
        string path = targetPathc.Replace(".Array.data", "");
        string[] fieldStructure = path.Split('.');

        targetObject = objectBeginningPath;
        parentObject = null;
        
        
        for (int i = 0; i < fieldStructure.Length; i++)
        {
            parentObject = targetObject;

            if (fieldStructure[i].Contains("["))
            {
                GetFieldValueFromCollection(fieldStructure[i], parentObject,out parentObject,out targetObject);
            }
            else
            {
                targetObject = GetFieldValue(fieldStructure[i], parentObject);
            }
        }
    }
    
    private static void GetFieldValueFromCollection(string fieldName, object  objectBeginningPath, out object parent, out object targetObject, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        Regex rgx = new Regex(@"\[\d+\]", RegexOptions.Compiled);
        
        string indexText = "";
        for (int j = 0; j < fieldName.Length; j++)
        {
            if (char.IsDigit(fieldName[j]))
            {
                indexText += fieldName[j];
            }
        }

        int index = Int32.Parse(indexText);
        string nameFiel = rgx.Replace(fieldName, "");

        
        parent = null;
        targetObject = default(object);
        
        FieldInfo field = objectBeginningPath.GetType().GetField(nameFiel, bindings);
        if (field != null)
        {
            parent = field.GetValue(objectBeginningPath);
            if (parent.GetType().IsArray)
            {
                targetObject = ((object[]) parent)[index];
                
            }
            else if (parent is IEnumerable)
            {
                targetObject = ((IList) parent)[index];
            }
        }

        
    }

    private static object GetFieldValue(string fieldName, object obj, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null)
        {
            return field.GetValue(obj);
        }
        return default(object);
    }

}
