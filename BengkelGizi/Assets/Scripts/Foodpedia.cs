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
    [SerializeField] private GameObject VitaminC;
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
    [SerializeField] private Sprite VitaminCDefault;
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
        setDefault(Nutrition);
    }

    private void InactiveAllNutrition()
    {
        Protein.SetActive(false);
        Karbohidrat.SetActive(false);
        Serat.SetActive(false);
        VitaminC.SetActive(false);
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
            setInfo("Ikan Tuna", ProteinDefault, "");
        }
        if (Nutrition == Karbohidrat)
        {
            setInfo("Roti", KarbohidratDefault, "Bread");
        }
        if (Nutrition == Serat)
        {
            setInfo("Buncis", SeratDefault, "Apple");
        }
        if (Nutrition == VitaminC)
        {
            setInfo("Jambu Biji", VitaminCDefault, "Avocado");
        }
        if (Nutrition == Kalsium)
        {
            setInfo("Keju", KalsiumDefault, "Almond");
        }
    }
}
