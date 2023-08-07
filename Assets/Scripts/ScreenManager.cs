using StarterAssets;
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
    public GameObject player;
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public ThirdPersonController playerController;
    Detection detection;
    ProgressBar progressBar;
    public TextMeshProUGUI percentSuspicionEndScreen;
    public TextMeshProUGUI percentSuspicionInGame;
    public List<GameObject> spawnPoints;

    public StageCompletionPhrasing completionPhraseScript;
    public bool phrasePicked = false;

    public int integerPercentSus;

    public Button advanceStage;
    public Button pauseButton;

    public int currentStage = 0;
    public int lastStage = 1;
    public bool isLastStage = false;

    void Start()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        gameOver.SetActive(false);
        stageOver.SetActive(false);
        detection = GameObject.FindGameObjectWithTag("character").GetComponent<Detection>();
        progressBar = GameObject.FindGameObjectWithTag("suspicionBar").GetComponent<ProgressBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        completionPhraseScript = GameObject.FindGameObjectWithTag("screenManager").GetComponent<StageCompletionPhrasing>();
    }

    void Update()
    {
        integerPercentSus = Mathf.RoundToInt(((progressBar.current / (float)progressBar.max) * 100));
        percentSuspicionInGame.text = integerPercentSus.ToString() + "% suspicion";

        if (Input.GetKey(KeyCode.Escape) && !stageOver.activeInHierarchy && !gameOver.activeInHierarchy && !homeScreen.activeInHierarchy)
        {
            pauseGame();
        }

        if (detection.isLost() == true)
        {
            playerController.disabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            gameOver.SetActive(true);
            pauseButton.interactable = false;
            Time.timeScale = 0f;
        }

        if (detection.stageComplete == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);

            isLastStage = currentStage == lastStage ? true : false;
            advanceStage.interactable = isLastStage ? false : true;

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
            pauseButton.interactable = false;

            if (phrasePicked == false)
            {
                completionPhraseScript.pickPhrase();
                phrasePicked = true;
            }

            Time.timeScale = 0f;
            playerController.disabled = true;
        }
    }

    public void goHome()
    {
        Time.timeScale = 0f;
        homeScreen.SetActive(true);
        pauseButton.interactable = false;
        pauseScreen.SetActive(false);
        stageOver.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        detection.stageComplete = false;
    }

    public void pauseGame()
    {
        playerController.disabled = true;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        pauseButton.interactable = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public void resumeGame()
    {
        playerController.disabled = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        pauseButton.interactable = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void newGame()
    {
        currentStage = 0;
        Time.timeScale = 1f;
        StartCoroutine("Teleport");
        homeScreen.SetActive(false);
        pauseButton.interactable = true;
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        progressBar.current = 0;
        progressBar.fillSuspicionBar();
        phrasePicked = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void nextStage()
    {
        currentStage = isLastStage ? 0 : currentStage + 1;

        Time.timeScale = 1f;
        StartCoroutine("Teleport");
        detection.stageComplete = false;
        stageOver.SetActive(false);
        pauseButton.interactable = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        progressBar.current = 0;
        progressBar.fillSuspicionBar();
        phrasePicked = false;
    }

    public void restartStage()
    {
        Time.timeScale = 1f;
        StartCoroutine("Teleport");
        detection.stageComplete = false;
        pauseButton.interactable = true;
        pauseScreen.SetActive(false);
        stageOver.SetActive(false);
        gameOver.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        progressBar.current = 0;
        progressBar.fillSuspicionBar();
        phrasePicked = false;
    }

    IEnumerator Teleport()
    {
        playerController.disabled = true;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = spawnPoints[currentStage].transform.position;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        detection.completedCounter = 0; 
        playerController.disabled = false;
    }
}
