using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    private Vector3 offset;

    private Vector3 offsetRotated;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        if (followTransform == null) return;
        offset = transform.position - followTransform.position;
    }

    private void Update()
    {
        FollowTransform();
    }

    private void FollowTransform()
    {
        if (followTransform == null) return;
        offsetRotated = followTransform.rotation * offset;
        transform.position = followTransform.position + offsetRotated;
        //transform.rotation = Quaternion.Slerp(transform.rotation, followTransform.rotation * startRotation, Time.deltaTime);
        transform.rotation = followTransform.rotation * startRotation;
    }
}
