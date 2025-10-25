using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Универсальный скрипт для движения и вращения обьектов через RB
/// С визуализацией направления движ. и вращения
/// </summary>
public class UniversalAddForceRb3D : MonoBehaviour
{
    [Header("Position Settings")]
    [SerializeField]
    private ForceMode _forceModePosition;
    [SerializeField]
    private Vector3 _directionPositon;
    [SerializeField]
    private Vector3 _focePositon = Vector3.one;
     
    [Header("Rotation Settings")]
    [SerializeField]
    private ForceMode _forceModeRotation;
    [SerializeField]
    private Vector3 _directionRotation;
    [SerializeField]
    private Vector3 _foceRotation = Vector3.one;

    [Header("List Rb")]
    [SerializeField]
    private List<Rigidbody> _targetObjectRb = new List<Rigidbody>();
    
    private void FixedUpdate()
    {
        foreach (var VARIABLE in _targetObjectRb)
        {
            Vector3 focePos = Vector3.Scale(_directionPositon.normalized, _focePositon);
            Vector3 foceRot = Vector3.Scale(_directionRotation.normalized, _foceRotation);
            
            // При исп. ForceMode.Force или Acceleration unity сама добавл в вычесления Time.fixedDeltaTime
            if (_forceModePosition == ForceMode.Force || _forceModePosition == ForceMode.Acceleration)
            {
                VARIABLE.AddForce(focePos, _forceModePosition);
            }
            //Но при режимах Impulse и VelocityChange он не берет в расчет Time.fixedDeltaTime, и это ладно если вызыв. 1 или 2 раза...
            //Но если буду вызвать постоянно.... в FixedUpdate, то ошибка накопиться 
            else
            {
                VARIABLE.AddForce(focePos * Time.fixedDeltaTime, _forceModePosition);
            }
            
            if (_forceModeRotation == ForceMode.Force || _forceModeRotation == ForceMode.Acceleration)
            {
                VARIABLE.AddTorque(foceRot, _forceModeRotation);
            }
            else
            {
                VARIABLE.AddTorque(foceRot * Time.fixedDeltaTime, _forceModeRotation);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var rb in _targetObjectRb)
        {
            if (rb != null)
            {
                Gizmos.DrawLine(rb.position, rb.position + _directionPositon.normalized * 2f);
            }
        }
        
        Gizmos.color = Color.red;
        foreach (var rb in _targetObjectRb)
        {
            if (rb != null)
            {
                // итоговый сила вращение с вектором направления
                Vector3 torque = Vector3.Scale(_directionRotation, _foceRotation);

                if (torque != Vector3.zero)
                {
                    // ось вращения
                    Vector3 axis = torque.normalized;

                    // радиус дуги (с учетом силы)
                    float radius = Mathf.Clamp(torque.magnitude, 0.2f, 2.5f);

                    // базовое направление для рисования дуги (относ. верха)
                    Vector3 startDir = Vector3.Cross(axis, Vector3.up);
                    if (startDir == Vector3.zero)
                    {
                        startDir = Vector3.Cross(axis, Vector3.forward);
                    }

                    // дуга вращения
                    int segments = 16;
                    float angle = 180f * Mathf.Sign(Vector3.Dot(axis, _directionRotation)); 
                    Vector3 prevPoint = rb.position + startDir * radius;
                    
                    for (int i = 1; i <= segments; i++)
                    {
                        float t = (i / (float)segments) * angle;
                        Quaternion rot = Quaternion.AngleAxis(t, axis);
                        Vector3 nextPoint = rb.position + rot * startDir * radius;
                        Gizmos.DrawLine(prevPoint, nextPoint);
                        prevPoint = nextPoint;
                    }
                }
            }
        }
    }
}
