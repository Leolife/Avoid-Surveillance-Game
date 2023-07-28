using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SinglePath_CameraBotController : MonoBehaviour
{
    public ScreenManager screenManager;
    public Detection detect;
    public ThirdPersonController thirdPersonController;
    public StarterAssetsInputs starterAssetsInputs;

    public GameObject[] cameraBot;
    public float speed = 1;
    public float secondsMoving = 1.5f;

    public bool isCoroutineStarted = false;
    public bool right = true;
    Vector3 goRight = Vector3.right;
    Vector3 goLeft = Vector3.left;

    void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("screenManager").GetComponent<ScreenManager>();
        detect = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
        thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        starterAssetsInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (screenManager.stageLevel == 1)
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
                Vector3 direction = right ? goRight : goLeft;
                cameraBot.transform.Translate(direction * speed * Time.deltaTime);

                if (!isCoroutineStarted)
                {
                    StartCoroutine(travelTime());
                }
            }
        }
    }

    IEnumerator travelTime()
    {
        Debug.Log("translation started at: " + Time.time);

        isCoroutineStarted = true;
        yield return new WaitForSeconds(secondsMoving);
        right = right ? false : true;
        isCoroutineStarted = false;

        Debug.Log("translation ended at: " + Time.time);
    }
}
