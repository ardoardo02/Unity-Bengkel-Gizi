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
            setInfo("Ikan Tuna", ProteinDefault, "Tuna merupakan ikan air laut yang berasal dari bangsa Thunnini. Daging ikan tuna memiliki warna merah muda sampai merah tua karena ototnya yang memiliki banyak myoglobin. Per 100 g BDD, tuna memiliki kandungan protein sebesar 28 g.");
        }
        if (Nutrition == Karbohidrat)
        {
            setInfo("Roti", KarbohidratDefault, "Roti merupakan makanan yang dibuat dengan bahan dasar utama tepung terigu dan air. Per 100 g BDD, roti memiliki kandungan karbohidrat sebesar 49 g.");
        }
        if (Nutrition == Serat)
        {
            setInfo("Buncis", SeratDefault, "Buncis adalah tanaman kacang-kacangan yang dipercayai berasal dari Amerika Tengah dan Amerika Selatan. Per 100 g, buncis memiliki kandungan serat sebesar 3.4 g");
        }
        if (Nutrition == VitaminC)
        {
            setInfo("Jambu Biji", VitaminCDefault, "Jamu biji adalah buah tropis yang berasal dari Amerika Tengah dan Amerika Selatan. Per 100 g, jambu biji memiliki kandungan Vitamin C sebesar 228,3 g.");
        }
        if (Nutrition == Kalsium)
        {
            setInfo("Keju", KalsiumDefault, "Keju merupakan salah satu produk olahan dari susu, biasanya susu sapi, kerbau, kambing, atau domba, melalui proses pengentalan atau koagulasi. Keju memiliki kandungan kalsium sebesar 721 mg.");
        }
    }
}
