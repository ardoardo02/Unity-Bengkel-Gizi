using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [SerializeField] private float heart =  5f;

    [SerializeField] private int CustomerTotal;
    [SerializeField] private int CustomerRemaining;
    private int CustomerRemainingText;
    [SerializeField] private int CustomerCounter;

    [SerializeField]private GameObject InfoBeforeStart_panel;
    [SerializeField] private GameObject GameOver_Panel;
    [SerializeField] private GameObject Victory_Panel;

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

        GameOver_Panel.SetActive(false);
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
        Victory_Panel.SetActive(true);
        Debug.Log("Victory");
    }


    private void GameOver()
    {
        GameOver_Panel.SetActive(true);
        Debug.Log("GameOver");
    }
    
}
