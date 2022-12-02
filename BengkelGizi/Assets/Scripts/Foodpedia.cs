using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Foodpedia : MonoBehaviour
{
    [Header("InfoBox Setup")]
    [SerializeField] private TMP_Text TitleInfo;
    [SerializeField] private Image ImageInfo;
    [SerializeField] private TMP_Text Description;
    [Header("Foodpedia Setup")]
    [SerializeField] private GameObject Protein;
    [SerializeField] private GameObject Karbohidrat;
    [SerializeField] private GameObject Serat;
    [SerializeField] private GameObject Mineral;
    [SerializeField] private GameObject Kalsium;


    [System.Serializable]
    public class FoodDefault
    {
        public string Title;
        public Sprite mage;
        public string Description;
    }

    [Header("Default Food Page")]
    [SerializeField] private Sprite ProteinDefault;
    [SerializeField] private Sprite KarbohidratDefault;
    [SerializeField] private Sprite SeratDefault;
    [SerializeField] private Sprite MineralDefault;
    [SerializeField] private Sprite KalsiumDefault;

    private void Start()
    {
        InactiveAllNutrition();
        //agar tidak kosong
        showNutrition(Protein);
    }
    public void showNutrition(GameObject Nutrition)
    {
        InactiveAllNutrition();
        Nutrition.SetActive(true);
        Debug.Log(Nutrition.ToString());
        setDefault(Nutrition);
    }

    private void InactiveAllNutrition()
    {
        Protein.SetActive(false);
        Karbohidrat.SetActive(false);
        Serat.SetActive(false);
        Mineral.SetActive(false);
        Kalsium.SetActive(false);
    }

    public void setFoodTitle(string Title)
    {
        TitleInfo.SetText(Title);
    }

    public void setFoodImage(Sprite foodSprite)
    {
        ImageInfo.sprite = foodSprite;
    }


    public void setFoodDescription(string Title)
    {
        Description.SetText(Title);
    }

    private void setInfo(string Title, Sprite Image, string Description)
    {
        setFoodDescription(Description);
        setFoodImage(Image);
        setFoodTitle(Title);
    }

    private void setDefault(GameObject Nutrition)
    {
        if (Nutrition == Protein)
        {
            setInfo("Daging Ayam", ProteinDefault, "Daging Ayam");
        }
        if (Nutrition == Karbohidrat)
        {
            setInfo("Bread", KarbohidratDefault, "Bread");
        }
        if (Nutrition == Serat)
        {
            setInfo("Apple", SeratDefault, "Apple");
        }
        if (Nutrition == Mineral)
        {
            setInfo("Avocado", MineralDefault, "Avocado");
        }
        if (Nutrition == Kalsium)
        {
            setInfo("Almond", KalsiumDefault, "Almond");
        }
    }
}
