using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static Rocket instance;
    public Transform targetTransform;
    public Vector3 TargetRot;
    public Vector3 RotationOffset;
    public Vector3 TargetPosition;
    public ParticleSystem ThrustPar;
    public PastLineRender Trail;
    public float InterpolationSpeed = 30;
 
    // Start is called before the first frame update
    void Awake()
    {
        TargetRot = Vector3.zero;
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tp = TargetPosition;

        tp.y = Mathf.Max(tp.y, 0);
        tp += Vector3.up * 7.621551f;

        float speed = InterpolationSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(transform.position, tp, speed);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetTransform.localRotation, speed);
    }

    public void setTargetRotation(Vector3 rotation)
    {
        targetTransform.eulerAngles = RotationOffset;
        targetTransform.Rotate(Vector3.up, rotation.y);
        targetTransform.Rotate(Vector3.forward, rotation.z);
        targetTransform.Rotate(Vector3.right, rotation.x);
    }

    public void SetTrailState(bool state)
    {
        ParticleSystem.EmissionModule em = ThrustPar.emission;
        em.enabled = state;
    }


    public void ResetTrail()
    {
        Trail.ClearLine();
    }



}
