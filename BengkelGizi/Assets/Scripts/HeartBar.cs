using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameManager.Heart);
        totalHealthBar.fillAmount = gameManager.Heart / 10;
        Debug.Log(gameManager.Heart / 10);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = gameManager.Heart / 10;
    }
}