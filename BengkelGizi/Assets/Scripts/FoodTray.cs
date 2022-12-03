using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FoodTray : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] SpriteRenderer foodtrayRender;
    [SerializeField] Transform foodsPlace;

    [Header("Nutrition Text")]
    [SerializeField] TMP_Text karboText;
    [SerializeField] TMP_Text proteinText;
    [SerializeField] TMP_Text seratText;
    [SerializeField] TMP_Text mineralText;
    [SerializeField] TMP_Text kalsiumText;

    Dictionary<Nutrition, int> plateValue = new Dictionary<Nutrition, int>(5){
        {Nutrition.Karbohidrat, 0},
        {Nutrition.Protein, 0},
        {Nutrition.Serat, 0},
        {Nutrition.Mineral, 0},
        {Nutrition.Kalsium, 0}
    };

    int karbo, protein_serat, mineral_kalsium = 0;
    bool freePlate = true;
    bool isServing = false;
    Transform placePoint;

    public List<Food> Foods = new List<Food>();

    public void AddFood(Food food, Transform foodRender)
    {
        if (isServing)
        {
            return;
        }

        if (Foods.Count >= 6 || IsPlateFull(food.NutritionValue))
        {
            audioManager.PlayPlateFullSFX();
            return;
        }

        audioManager.PlayClickFoodSFX();

        // Ngisi nilai makanan yang ada di piring
        UpdateNutrition(food.NutritionValue, food.FoodValue);

        if (((karbo == 1 && food.NutritionValue == Nutrition.Karbohidrat)
                || (protein_serat == 2 && (food.NutritionValue == Nutrition.Protein || food.NutritionValue == Nutrition.Serat))
                || (mineral_kalsium == 2 && (food.NutritionValue == Nutrition.Mineral || food.NutritionValue == Nutrition.Kalsium)))
            && freePlate)
        {
            placePoint = foodsPlace.GetChild(5);
            freePlate = false;
        }
        else if (food.NutritionValue == Nutrition.Karbohidrat)
        {
            placePoint = foodsPlace.GetChild(0);
            karbo++;
        }
        else if (food.NutritionValue == Nutrition.Protein || food.NutritionValue == Nutrition.Serat)
        {
            placePoint = foodsPlace.GetChild(protein_serat + 1);
            protein_serat++;
        }
        else
        {
            placePoint = foodsPlace.GetChild(mineral_kalsium + 3);
            mineral_kalsium++;
        }

        // spawn makanan di food tray
        placePoint.GetComponent<SpriteRenderer>().sprite = foodRender.GetComponent<SpriteRenderer>().sprite;

        // add makanan ke list
        Foods.Add(food);
    }

    private bool IsPlateFull(Nutrition val)
    {
        if (karbo == 0 && protein_serat == 0 && mineral_kalsium == 0)
            return false;

        if (val == Nutrition.Karbohidrat && karbo == 1)
        {
            if (freePlate)
                return false;
            return true;
        }

        if ((val == Nutrition.Protein || val == Nutrition.Serat) && protein_serat == 2)
        {
            if (freePlate)
                return false;
            return true;
        }

        if ((val == Nutrition.Mineral || val == Nutrition.Kalsium) && mineral_kalsium == 2)
        {
            if (freePlate)
                return false;
            return true;
        }

        return false;
    }

    private void UpdateNutrition(Nutrition nutrition, int val)
    {
        plateValue[nutrition] += val;

        karboText.text = plateValue[Nutrition.Karbohidrat].ToString();
        proteinText.text = plateValue[Nutrition.Protein].ToString();
        seratText.text = plateValue[Nutrition.Serat].ToString();
        mineralText.text = plateValue[Nutrition.Mineral].ToString();
        kalsiumText.text = plateValue[Nutrition.Kalsium].ToString();
    }

    private void ResetPlate()
    {
        isServing = false;
        ChangeFoodtrayColor();
        audioManager.PlayResetPlateSFX();

        plateValue[Nutrition.Karbohidrat] = 0;
        plateValue[Nutrition.Protein] = 0;
        plateValue[Nutrition.Serat] = 0;
        plateValue[Nutrition.Mineral] = 0;
        plateValue[Nutrition.Kalsium] = 0;

        karbo = 0;
        protein_serat = 0;
        mineral_kalsium = 0;
        freePlate = true;

        UpdateNutrition(nutrition: Nutrition.Karbohidrat, val: 0);
        Foods.Clear();

        for (int i = 0; i < foodsPlace.childCount; i++)
        {
            foodsPlace.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    public string ServeFood(Customer cus)
    {
        if (!isServing)
            return "Not Serving";

        foreach (var (key, value) in cus.CustOrder)
        {
            if (cus.CustOrder[key] != plateValue[key])
            {
                ResetPlate();
                return "Wrong";
            }
        }

        ResetPlate();
        return "Correct";
    }

    private void ChangeFoodsColor(Color color, bool isMaterial)
    {
        for (int i = 0; i < foodsPlace.childCount; i++)
        {
            if (isMaterial)
                foodsPlace.GetChild(i).GetComponent<SpriteRenderer>().material.color = color;
            else
                foodsPlace.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void ChangeFoodtrayColor()
    {
        if (isServing)
        {
            foodtrayRender.color = Color.gray;
            ChangeFoodsColor(Color.gray, false);
        }
        else
        {
            foodtrayRender.color = Color.white;
            ChangeFoodsColor(Color.white, false);
        }
    }

    private void OnMouseDown()
    {
        foodtrayRender.material.color = Color.white;
        ChangeFoodsColor(Color.white, true);

        if (karbo == 0 && protein_serat == 0 && mineral_kalsium == 0)
        {
            // audioManager.PlayPlateEmptySFX();
            return;
        }

        isServing = !isServing;
        ChangeFoodtrayColor();
        audioManager.PlayClickFoodtraySFX();

        // audioManager.PlayClickFoodSFX();

        // foodTray.AddFood(this, transform.GetChild(0));
    }

    private void OnMouseUp()
    {
        foodtrayRender.material.color = new Color(0.9f, 0.9f, 0.9f);
        ChangeFoodsColor(new Color(0.9f, 0.9f, 0.9f), true);
    }

    private void OnMouseEnter()
    {
        foodtrayRender.material.color = new Color(0.9f, 0.9f, 0.9f);
        ChangeFoodsColor(new Color(0.9f, 0.9f, 0.9f), true);
    }

    private void OnMouseExit()
    {
        foodtrayRender.material.color = Color.white;
        ChangeFoodsColor(Color.white, true);
    }
}
