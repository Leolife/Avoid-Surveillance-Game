using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraBotController : MonoBehaviour
{
    public ScreenManager screenManager;
    public Detection detect;
    public ThirdPersonController thirdPersonController;
    public StarterAssetsInputs starterAssetsInputs;

    public GameObject[] cameraBot;
    public float speed = 1;
    public float rotationSpeed = 1f;

    public bool isPatrolling = true;
    public bool isCoroutineStarted = false;

    public float currentRotationAmount;
    public float targetRotationAmount;

    void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("screenManager").GetComponent<ScreenManager>();
        detect = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
        thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        starterAssetsInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (screenManager.currentStage == 1)
        {
            if (detect.detected)
            {
                thirdPersonController.MoveSpeed = 1.5f;
                thirdPersonController._speed = thirdPersonController.MoveSpeed;
                starterAssetsInputs.sprint = false;
            }
            else
            {
                thirdPersonController.MoveSpeed = 2.6675f;
            }

            foreach (var cameraBot in cameraBot)
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
        }
        
    }

    void Patrol()
    {
        foreach (var cameraBot in cameraBot)
        {
            Vector3 currentPosition = cameraBot.transform.position;
            cameraBot.transform.position = currentPosition + cameraBot.transform.forward * speed * Time.deltaTime;
        }
    }

    void updateRotation()
    {
        foreach (var cameraBot in cameraBot)
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
