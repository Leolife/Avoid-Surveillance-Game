using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject cameraObject;
    public float largeTargetRotationY = 327.0f;
    public float smallTargetRotationY = 210.0f;
    public float rotationSpeed = 0.1f;
    private bool rotateClockwise = true;

    private void Start()
    {
        cameraObject = GameObject.FindGameObjectWithTag("movingCamera");
    }

    private void Update()
    {
        float rotationDirection;
        float currentRotationY = cameraObject.transform.rotation.eulerAngles.y;

        if (Mathf.Approximately(currentRotationY, largeTargetRotationY) || Mathf.Approximately(currentRotationY, smallTargetRotationY))
        {
            rotateClockwise = !rotateClockwise;
        }

        if (rotateClockwise)
        {
            rotationDirection = 1.0f;
        }
        else
        {
            rotationDirection = -1.0f;
        }

        cameraObject.transform.Rotate(0.0f, rotationSpeed * rotationDirection, 0.0f, Space.Self);
    }
}
