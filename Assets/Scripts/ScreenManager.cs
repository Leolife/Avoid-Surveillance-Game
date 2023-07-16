using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public GameObject stageOver;
    public GameObject star1, star2, star3;
    public GameObject gameOver;
    Detection detection;
    ProgressBar progressBar;
    public TextMeshProUGUI percentSuspicion;

    public int integerPercentSus;

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
            integerPercentSus = Mathf.RoundToInt(((progressBar.current / (float)progressBar.max) * 100));
            if (integerPercentSus <= 10)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if (integerPercentSus > 10 && integerPercentSus <= 40)
            {
                star1.SetActive(true);
                star2.SetActive(true);
            }
            else if (integerPercentSus > 40)
            {
                star1.SetActive(true);
            }
            percentSuspicion.text = integerPercentSus.ToString() + "% suspicion";
            stageOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
