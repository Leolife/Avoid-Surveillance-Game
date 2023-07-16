using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public GameObject stageOver;
    public GameObject gameOver;
    Detection detection;
    ProgressBar progressBar;
    public TextMeshProUGUI percentSuspicion;

    void Start()
    {
        gameOver.SetActive(false);
        stageOver.SetActive(false);
        detection = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
        progressBar = GameObject.FindGameObjectWithTag("suspicionBar").GetComponent<ProgressBar>();
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
            percentSuspicion.text = Mathf.RoundToInt(((progressBar.current / (float)progressBar.max) * 100)).ToString() + "% suspicion";
            stageOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
