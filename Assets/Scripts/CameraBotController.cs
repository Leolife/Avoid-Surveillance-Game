using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraBotController : MonoBehaviour
{
    public GameObject cameraBot;
    public float speed = 1;
    public float rotationSpeed = 1f;

    public bool isPatrolling = true;
    public bool isCoroutineStarted = false;

    public float currentRotationAmount;
    public float targetRotationAmount;


    void Update()
    {
        if (isPatrolling)
        {
            currentRotationAmount = Mathf.Round((float)cameraBot.transform.rotation.eulerAngles.y);
            if (Mathf.Round(currentRotationAmount) == 270)
            {
                targetRotationAmount = Mathf.Round(0);
            }
            else
            {
                targetRotationAmount = Mathf.Round(currentRotationAmount + 90);
            }

            Patrol();
            if (!isCoroutineStarted)
            {
                StartCoroutine(patrolTime());
            }
        }
        else
        {
            updateRotation();
            if (Mathf.Round(currentRotationAmount) == targetRotationAmount)
            {
                isPatrolling = true;
                isCoroutineStarted = false;
            }
        }
    }

    void Patrol()
    {
        Vector3 currentPosition = cameraBot.transform.position;
        cameraBot.transform.position = currentPosition + cameraBot.transform.forward * speed * Time.deltaTime;
    }

    void updateRotation()
    {
        if (currentRotationAmount < targetRotationAmount && currentRotationAmount != 270)
        {
            cameraBot.transform.Rotate(0, rotationSpeed, 0);
            currentRotationAmount = (float)cameraBot.transform.rotation.eulerAngles.y;
        }
        else
        {
            cameraBot.transform.Rotate(0, rotationSpeed, 0);
            currentRotationAmount = (float)cameraBot.transform.rotation.eulerAngles.y;
        }
    }

    IEnumerator patrolTime()
    {
        Debug.Log("rotation started at: " + Time.time);

        isCoroutineStarted = true;
        yield return new WaitForSeconds(1.5f);
        isPatrolling = false;

        Debug.Log("rotation ended at: " + Time.time);
    }
}
