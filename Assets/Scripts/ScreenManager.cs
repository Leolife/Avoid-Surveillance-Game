using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject stageOver; //NEW
    public GameObject gameOver;
    Detection detection;

    void Start()
    {
        gameOver.SetActive(false);
        stageOver.SetActive(false);
        detection = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
    }

    void Update()
    {
        if (detection.isLost() == true)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0f;
        }
        if (detection.stageComplete == true)
        {
            stageOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
