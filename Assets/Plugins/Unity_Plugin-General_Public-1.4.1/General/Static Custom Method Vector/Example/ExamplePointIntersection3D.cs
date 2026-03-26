using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePointIntersection3D : MonoBehaviour
{
    [SerializeField] 
    private GameObject _point1;
    
    [SerializeField] 
    private GameObject _point2;
    
    private Vector3 _pointIntersectionLine1 = Vector3.zero;
	private Vector3 _pointIntersectionLine2 = Vector3.zero;
    
    private void FixedUpdate()
    {
        Vector3 pointIntersectionLine1 = Vector3.zero;
        Vector3 pointIntersectionLine2 = Vector3.zero;
        
        //нужны т.к нахожу не относ. X, а относительно другой плоскости
        Vector3 pointIntersectionLine_1NormalXYZ;
        Vector3 pointIntersectionLine_2NormalXYZ;
        
        //работает нормально
        StaticCustomMethodVector.FindPointIntersectionV3(
            new Vector3(_point1.transform.position.x, _point1.transform.position.z, _point1.transform.position.y),
            new Vector3(_point1.transform.forward.x, _point1.transform.forward.z, _point1.transform.forward.y),
            new Vector3(_point2.transform.position.x, _point2.transform.position.z, _point2.transform.position.y),
            new Vector3(_point2.transform.forward.x, _point2.transform.forward.z, _point2.transform.forward.y),
            out pointIntersectionLine1,out pointIntersectionLine2);

        pointIntersectionLine_1NormalXYZ = new Vector3(pointIntersectionLine1.x, pointIntersectionLine1.z, pointIntersectionLine1.y);
        pointIntersectionLine_2NormalXYZ = new Vector3(pointIntersectionLine2.x, pointIntersectionLine2.z, pointIntersectionLine2.y);
        
        //тест (тоже работает)
        // StaticCustomMethodVector.FindPointIntersectionV3(
        //     new Vector3(_point1.transform.position.z, _point1.transform.position.x, _point1.transform.position.y),
        //     new Vector3(_point1.transform.forward.z, _point1.transform.forward.x, _point1.transform.forward.y),
        //     new Vector3(_point2.transform.position.z, _point2.transform.position.x, _point2.transform.position.y),
        //     new Vector3(_point2.transform.forward.z, _point2.transform.forward.x, _point2.transform.forward.y),
        //     out tttttt,out tttttt2);
        //
        //
        // _gloabal4 = new Vector3(tttttt.y, tttttt.z, tttttt.x);
        // _gloabal5 = new Vector3(tttttt2.y, tttttt2.z, tttttt2.x);

        //тест (не работает)(скорее всего тут уже проекция идет по другой плоскости, но мне в падлу считать)
        // StaticCustomMethodVector.FindPointIntersectionV3(
        //     new Vector3(_point1.transform.position.z, _point1.transform.position.y, _point1.transform.position.x),
        //     new Vector3(_point1.transform.forward.z, _point1.transform.forward.y, _point1.transform.forward.x),
        //     new Vector3(_point2.transform.position.z, _point2.transform.position.y, _point2.transform.position.x),
        //     new Vector3(_point2.transform.forward.z, _point2.transform.forward.y, _point2.transform.forward.x),
        //     out tttttt,out tttttt2);
        //
        //(именно тут не уверен к координатах, мог и перепутать)
        // _gloabal4 = new Vector3(tttttt.x, tttttt.z, tttttt.y);
        // _gloabal5 = new Vector3(tttttt2.x, tttttt2.z, tttttt2.y);

        //тест (не работает нормельно)
        // StaticCustomMethodVector.FindPointIntersectionV3(
        //     new Vector3(_point1.transform.position.x, _point1.transform.position.y, _point1.transform.position.z),
        //     new Vector3(_point1.transform.forward.x, _point1.transform.forward.y, _point1.transform.forward.z),
        //     new Vector3(_point2.transform.position.x, _point2.transform.position.y, _point2.transform.position.z),
        //     new Vector3(_point2.transform.forward.x, _point2.transform.forward.y, _point2.transform.forward.z),
        //     out tttttt,out tttttt2);
        //
        //
        // _gloabal4 = new Vector3(tttttt.x, tttttt.y, tttttt.z);
        // _gloabal5 = new Vector3(tttttt2.x, tttttt2.y, tttttt2.z);
        
        
        Debug.Log("Точка пересечения на линии 1 X = " + pointIntersectionLine_1NormalXYZ.x + " | на линии 2 X = " + pointIntersectionLine_2NormalXYZ.x);
        Debug.Log("Точка пересечения на линии 1 y = " + pointIntersectionLine_1NormalXYZ.y + " | на линии 2 y = " + pointIntersectionLine_2NormalXYZ.y);
        Debug.Log("Точка пересечения на линии 1 z = " + pointIntersectionLine_1NormalXYZ.z + " | на линии 2 Z = " + pointIntersectionLine_2NormalXYZ.z);

        _pointIntersectionLine1 = pointIntersectionLine_1NormalXYZ;
        _pointIntersectionLine2 = pointIntersectionLine_2NormalXYZ;

        //Просто выведет информацию об точках пересечения
        StaticCustomMethodVector.LinePointIntersectionInfo(pointIntersectionLine_1NormalXYZ, pointIntersectionLine_2NormalXYZ, 4);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (_point1 != null)
        {
            Gizmos.DrawRay(_point1.transform.position, _point1.transform.forward*100f);    
        }
        
        if (_point2 != null)
        {
            Gizmos.DrawRay(_point2.transform.position, _point2.transform.forward*100f);    
        }


        Color col;
       
        
        col = Color.green;
        col.a = 0.5f;
        Gizmos.color = col;
        Gizmos.DrawSphere(_pointIntersectionLine1,0.1f);

        col = Color.red;
        col.a = 0.5f;
        Gizmos.color = col;
        Gizmos.DrawSphere(_pointIntersectionLine2,0.1f);
        
    }
}
