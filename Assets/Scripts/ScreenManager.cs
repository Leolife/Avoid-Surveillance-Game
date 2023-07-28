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
    public TextMeshProUGUI percentSuspicionEndScreen;
    public TextMeshProUGUI percentSuspicionInGame;
    public List<GameObject> spawnPoints;

    public int integerPercentSus;
    public int stageLevel = 0;

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
        integerPercentSus = Mathf.RoundToInt(((progressBar.current / (float)progressBar.max) * 100));
        percentSuspicionInGame.text = integerPercentSus.ToString() + "% suspicion";

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

            percentSuspicionEndScreen.text = integerPercentSus.ToString() + "% suspicion";      
            stageOver.SetActive(true);
            Time.timeScale = 0f;
            playerController.disabled = true;
        }
    }

    public void nextStage()
    {
        stageLevel++;
        Time.timeScale = 1f;
        StartCoroutine("Teleport");
        detection.stageComplete = false;
        stageOver.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        progressBar.current = 0;
        progressBar.fillSuspicionBar();
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
        yield return new WaitForSeconds(0.01f);
        player.transform.position = new Vector3(5.3f, 0.5f, -6.5f);
        player.transform.position = spawnPoints[stageLevel].transform.position;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        detection.completedCounter = 0; 
        playerController.disabled = false;
    }
}
