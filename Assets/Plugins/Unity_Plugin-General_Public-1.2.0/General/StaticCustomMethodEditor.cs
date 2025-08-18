using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class StaticCustomMethodEditor
    {

        //Вернет список имен полей, которые есть в сериализованном классе(только именно в нем, не будет учитывать все возможные поля вложенных классов)
        public static List<string> GetNestedNamesProperty(SerializedProperty property)
        {
            List<string> list = new List<string>();

            //Обяз(так как иначе все идет по ....) (надо разобраться как там под капотом че работает)
            var copy = property.Copy();

            var copy2 = property.Copy();

            var enumerator = copy.GetEnumerator();

            while (enumerator.MoveNext())
            {
                SerializedProperty element = enumerator.Current as SerializedProperty;

                if (copy2.FindPropertyRelative(element.name) != null)
                {
                    list.Add(element.name);
                }

            }

            return list;
        }

        // Вернет список имен полей, которые есть в сериализованном классе(будет учитывать все возможные поля вложенных классов)
        public static List<string> GetNestedNamesPropertyFull(SerializedProperty property)
        {
            List<string> list = new List<string>();

            //Обяз(так как иначе все идет по ....) (надо разобраться как там под капотом че работает)
            var copy = property.Copy();

            var enumerator = copy.GetEnumerator();

            while (enumerator.MoveNext())
            {
                SerializedProperty element = enumerator.Current as SerializedProperty;

                list.Add(element.name);
            }

            return list;
        }

        //выключит возможность изменять UI во время выполнения action, а потом вернет, если UI был включен
        public static void DisactiveGUI(Action action)
        {
            if (GUI.enabled == true)
            {
                GUI.enabled = false;
                action.Invoke();
                GUI.enabled = true;
                return;
            }

            action.Invoke();
        }

        //Получение Object по известному TargetPath
        public static object GetTargetObjectPath(object objectBeginningPath, string targetPath,
            bool findFieldBaseType = false)
        {
            var targetObject = default(object);
            var parentObject = default(object);

            GetTargetObjectAndParentPath(objectBeginningPath, targetPath, out parentObject, out targetObject,
                findFieldBaseType);


            return targetObject;
        }

        //Получение Object и того класса в котором он находиться по известному TargetPath
        public static void GetTargetObjectAndParentPath(object objectBeginningPath, string targetPath,
            out object parentObject, out object targetObject, bool findFieldBaseType = false)
        {
            string path = targetPath.Replace(".Array.data", "");
            string[] fieldStructure = path.Split('.');

            targetObject = objectBeginningPath;
            parentObject = null;


            for (int i = 0; i < fieldStructure.Length; i++)
            {
                parentObject = targetObject;


                if (fieldStructure[i].Contains("["))
                {
                    GetFieldValueFromCollection(fieldStructure[i], parentObject, out parentObject, out targetObject,
                        findFieldBaseType);
                }
                else
                {
                    targetObject = GetFieldValue(fieldStructure[i], parentObject, findFieldBaseType);
                }

            }
        }

        private static void GetFieldValueFromCollection(string fieldName, object objectBeginningPath, out object parent,
            out object targetObject, bool findFieldBaseType = false,
            BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                    BindingFlags.NonPublic)
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

            if (field == null && findFieldBaseType == true)
            {
                field = FindFieldParentClassBaseType(objectBeginningPath, nameFiel, bindings);
            }

            if (field != null)
            {
                parent = field.GetValue(objectBeginningPath);
                if (parent.GetType().IsArray)
                {
                    targetObject = ((object[])parent)[index];
                }
                else if (parent is IEnumerable)
                {
                    targetObject = ((IList)parent)[index];
                }
            }


        }

        private static FieldInfo FindFieldParentClassBaseType(object obj, string nameField, BindingFlags bindings)
        {
            var parentClassBaseType = obj.GetType().BaseType;
            FieldInfo field = null;

            while (true)
            {
                if (parentClassBaseType != null)
                {
                    field = parentClassBaseType.GetField(nameField, bindings);
                    if (field != null)
                    {
                        return field;
                    }

                    parentClassBaseType = parentClassBaseType.BaseType;
                    continue;
                }
            }

            return field;
        }

        private static object GetFieldValue(string fieldName, object obj, bool findFieldBaseType = false,
            BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                    BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);

            if (field == null && findFieldBaseType == true)
            {
                field = FindFieldParentClassBaseType(obj, fieldName, bindings);
            }

            if (field != null)
            {
                return field.GetValue(obj);
            }


            return default(object);
        }



    }

#endif