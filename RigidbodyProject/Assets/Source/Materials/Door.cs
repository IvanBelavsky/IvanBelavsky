using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private HingeJoint _joint;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void Open()
    {
        _material.color = Color.blue;
        JointSpring jointPosition = _joint.spring;
        jointPosition.targetPosition = 180;
        _joint.spring = jointPosition;
    } 

    public void Close()
    {
        _material.color = Color.red;
        JointSpring jointPosition = _joint.spring;
        jointPosition.targetPosition = 0;
        _joint.spring = jointPosition;
    }
}
