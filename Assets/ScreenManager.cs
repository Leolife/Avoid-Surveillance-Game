using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject gameOver;
    Detection detection;

    void Start()
    {
        gameOver.SetActive(false);
        detection = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
    }

    void Update()
    {
        if (detection.isLost() == true)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
