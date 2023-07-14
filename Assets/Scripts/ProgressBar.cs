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
    void Start()
    {
        max = 10;
        current = 0;
    }

    void Update()
    {
        fillSuspicionBar();
    }

    public void fillSuspicionBar()
    {
        float progress = current / (float)max;
        Mask.fillAmount = progress;
    }
}
