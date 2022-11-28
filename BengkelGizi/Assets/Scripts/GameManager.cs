using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float heart =  5f;

    [SerializeField] private int CustomerTotal;
    [SerializeField] private int CustomerRemaining;
    private int CustomerRemainingText;
    [SerializeField] private int CustomerCounter;

    [SerializeField]private GameObject InfoBeforeStartPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject VictoryPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject DimmerPause;

    [SerializeField] private GameObject AreYourSureRestart;
    [SerializeField] private GameObject AreYourSureMainMenu;

    [SerializeField] private int level;
    [SerializeField] private TMP_Text LevelNum_txt;
    [SerializeField] private TMP_Text CustRemaining_txt;

    private static GameManager gameManagerInstance;
    public static GameManager Instance
    {
        get
        {
            if (gameManagerInstance == null)
                gameManagerInstance = FindObjectOfType<GameManager>();

            return gameManagerInstance;
        }
    }

    public float Heart { get => heart; set => heart = value; }

    private void Start()
    {
        LevelNum_txt.SetText(level.ToString());

        CustomerRemaining = CustomerTotal;

        CustomerRemainingText = CustomerTotal;
        CustRemaining_txt.SetText(CustomerRemainingText.ToString());

        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        VictoryPanel.SetActive(false);
        Time.timeScale = 0;
        InfoBeforeStartPanel.SetActive(true);
    }

    public void StartGame()
    {
        DimmerPause.SetActive(false);
        InfoBeforeStartPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void CustGone()
    {
        Heart -= 1;
        CustomerRemaining -= 1;
        if (Heart <= 0)
        {
            //Lose Condition
            Time.timeScale = 0;
            GameOver();
        }

        if (CustomerRemaining == 0) Victory();
    }

    public void CustServe()
    {
        Debug.Log("Served");
        CustomerRemaining -= 1;
        //win condition
        if (CustomerRemaining == 0)
        {
            Time.timeScale = 0;
            Victory();
        }
    }

    public void CustSpawn()
    {
        CustomerCounter += 1;
        CustomerRemainingText -= 1;
        CustRemaining_txt.SetText(CustomerRemainingText.ToString());
    }

    public bool CheckCustDone()
    {
        if (CustomerCounter == CustomerTotal)
        {
            return true;
        }
        return false;
    }

    private void Victory()
    {
        VictoryPanel.SetActive(true);
        Debug.Log("Victory");
    }


    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        Debug.Log("GameOver");
    }


    // Pause Panel
    public void PauseGame()
    {
        DimmerPause.SetActive(true);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        AreYourSureRestart.SetActive(true);
    }

    public void Foodpedia()
    {

    }

    public void Option()
    {

    }

    public void MainMenu()
    {
        AreYourSureMainMenu.SetActive(true);
    }

    public void AreYouSure(string Action)
    {
        if(Action == "Restart")
        {
            var currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
        if (Action == "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
