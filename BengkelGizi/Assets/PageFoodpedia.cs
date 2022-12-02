using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageFoodpedia : MonoBehaviour
{
    [Header("button Setup")]
    [SerializeField] private GameObject ProteinButton;
    [SerializeField] private GameObject KarbohidratButton;
    [SerializeField] private GameObject SeratButton;
    [SerializeField] private GameObject MineralButton;
    [SerializeField] private GameObject kalsiumButton;

    private void Start()
    {
        NutritionButton(ProteinButton);
    }   

    public void NutritionButton(GameObject NutritionButton)
    {
        ActivateAllButton();
        NutritionButton.SetActive(false);
    }

    private void ActivateAllButton()
    {
        ProteinButton.SetActive(true);
        KarbohidratButton.SetActive(true);
        SeratButton.SetActive(true);
        MineralButton.SetActive(true);
        kalsiumButton.SetActive(true);
    }
}
