using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay Setup")]
    [SerializeField] private float heart = 5f;
    [SerializeField] private int level;
    [SerializeField] private TMP_Text LevelNum_txt;
    [SerializeField] private GameObject InfoBeforeStart_panel;

    [Header("Customer Setup")]
    [SerializeField] private int CustomerTotal;
    [SerializeField] private int CustomerRemaining;
    private int CustomerRemainingText;
    [SerializeField] private int CustomerCounter;
    [SerializeField] private TMP_Text CustRemaining_txt;


    [Header("Victory Setup")]
    [SerializeField] private GameObject Victory_Panel;
    [SerializeField] private Button NextLevel_Button;

    [SerializeField] private Image Star;
    [SerializeField] private Sprite Star1;
    [SerializeField] private Sprite Star2;
    [SerializeField] private Sprite Star3;

    [SerializeField] private TMP_Text levelNum_txt;
    [SerializeField] private TMP_Text CustomerServed_txt;
    [SerializeField] private TMP_Text CustomerAngry_txt;

    [Header("Pause Menu Setup")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject AreYourSureRestart;
    [SerializeField] private GameObject AreYourSureMainMenu;




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
        Victory();
        //GameOver();
        LevelNum_txt.SetText(level.ToString());

        CustomerRemaining = CustomerTotal;

        CustomerRemainingText = CustomerTotal;
        CustRemaining_txt.SetText(CustomerRemainingText.ToString());

        PausePanel.SetActive(false);
        Victory_Panel.SetActive(false);

        Time.timeScale = 0;
        InfoBeforeStart_panel.SetActive(true);
    }

    public void StartGame()
    {
        InfoBeforeStart_panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void CustGone()
    {
        heart -= 1;
        CustomerRemaining -= 1;
        if (heart <= 0)
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
        Victory_Panel.SetActive(true);
        if (heart == 5)
        {
            Star.sprite = Star3;
        }
        else if (heart <= 4 && heart >= 3)
        {
            Star.sprite = Star2;
        }
        else if (heart <= 2 && heart >= 1)
        {
            Star.sprite = Star1;
        }

        setupTextWinLose();

        levelNum_txt.SetText(level.ToString() + " Complete");
        Debug.Log("Victory");
    }


    private void GameOver()
    {
        NextLevel_Button.interactable = false;
        Victory_Panel.SetActive(true);
        setupTextWinLose();
        levelNum_txt.SetText(level.ToString() + " Failed");
        Debug.Log("GameOver");
    }

    private void setupTextWinLose()
    {
        var lastHeart = 5 - heart;
        CustomerAngry_txt.SetText(lastHeart.ToString());
        var CustomerServed = CustomerTotal - lastHeart;
        CustomerServed_txt.SetText(CustomerServed.ToString());
    }
    // Pause Panel
    public void PauseGame()
    {
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
        if (Action == "Restart")
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
