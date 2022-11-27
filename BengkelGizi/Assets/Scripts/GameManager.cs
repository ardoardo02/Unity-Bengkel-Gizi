using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int heart =  5;
    public int CustomerTotal;
    public int CustomerRemaining;
    public int CustomerCounter;
    [SerializeField]private GameObject InfoBeforeStart_panel;
    //[SerializeField] CustomerManager cm;

    [SerializeField] private GameObject GameOver_Panel;
    [SerializeField] private GameObject Victory_Panel;

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

    private void Start()
    {
        GameOver_Panel.SetActive(false);
        Victory_Panel.SetActive(false);
        CustomerRemaining  = CustomerTotal;
        InfoBeforeStart_panel.SetActive(true);
        Time.timeScale = 0;
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
