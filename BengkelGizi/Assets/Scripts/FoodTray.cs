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
    [SerializeField] TMP_Text nutritionText;

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

        nutritionText.text = "Kb: " + plateValue[Nutrition.Karbohidrat] + " | " +
            "Pr: " + plateValue[Nutrition.Protein] + " | " +
            "Sr: " + plateValue[Nutrition.Serat] + " | " +
            "Mn: " + plateValue[Nutrition.Mineral] + " | " +
            "Kl: " + plateValue[Nutrition.Kalsium];
    }

    private void ResetPlate()
    {
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

    public void ServeFood(Customer cus)
    {
        if (!isServing)
            return;

        ResetPlate();

        foreach (var (key, value) in cus.CustOrder)
        {
            Debug.Log("(order) " + key + " " + value);
        }

        foreach (var (key, value) in plateValue)
        {
            Debug.Log("(plate) " + key + ": " + value);
        }
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

    private void OnMouseDown()
    {
        isServing = !isServing;

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

        foodtrayRender.material.color = Color.white;
        ChangeFoodsColor(Color.white, true);
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
