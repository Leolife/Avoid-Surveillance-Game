using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public GameObject stageOver;
    public GameObject star1, star2, star3;
    public GameObject gameOver;
    public GameObject player;
    public ThirdPersonController playerController;
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
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
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

    public void restartStage()
    {
        Time.timeScale = 1f;
        StartCoroutine("Teleport");
        detection.stageComplete = false;
        stageOver.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        progressBar.current = 0;
        progressBar.fillSuspicionBar();
    }

    IEnumerator Teleport()
    {
        playerController.disabled = true;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = new Vector3(5.3f, 3f, -6.5f);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        detection.completedCounter = 0; 
        playerController.disabled = false;
    }
}
