using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class RotateCamera : MonoBehaviour
{
    public GameObject cameraObject;
    public float largeTargetRotationY = 327;
    public float smallTargetRotationY = 210;
    public float rotationSpeed = 0.1f;
    private bool rotateClockwise = true;
    float rotationDirection = 1;


    private void Start()
    {
        cameraObject = GameObject.FindGameObjectWithTag("movingCamera");

    }

    private void Update()
    {
        float currentRotationY = cameraObject.transform.rotation.eulerAngles.y;

        if (Mathf.Approximately(currentRotationY, largeTargetRotationY) || Mathf.Approximately(currentRotationY, smallTargetRotationY))
        {
            rotateClockwise = !rotateClockwise;
            rotationDirection = rotateClockwise ? 1 : -1;
        }

        cameraObject.transform.Rotate(0.0f, rotationSpeed * rotationDirection, 0.0f, Space.Self);
    }
}
