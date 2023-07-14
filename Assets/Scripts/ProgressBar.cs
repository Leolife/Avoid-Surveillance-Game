using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int max;
    public float current;
    public Image Mask;

    Detection detect;

    void Start()
    {
        detect = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
        max = 10;
        current = 0;
    }

    void Update()
    {
        if (detect.detected) //this if statement optimizes for FPS
        {
            fillSuspicionBar();
        }
    }

    public void fillSuspicionBar()
    {
        float progress = current / (float)max;
        Mask.fillAmount = progress;
    }
}
