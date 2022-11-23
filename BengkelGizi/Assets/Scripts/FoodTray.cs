using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    [SerializeField] Transform foodsParent;
    [SerializeField] Transform foodsPlace;

    Dictionary<Nutrition, int> plateValue = new Dictionary<Nutrition, int>(5){
        {Nutrition.Karbohidrat, 0},
        {Nutrition.Protein, 0},
        {Nutrition.Serat, 0},
        {Nutrition.Mineral, 0},
        {Nutrition.Kalsium, 0}
    };

    int karbo, protein_serat, mineral_kalsium = 0;
    Transform placePoint;

    public List<Food> Foods = new List<Food>();

    public void AddFood(Food food)
    {
        if (Foods.Contains(food) || Foods.Count >= 5)
            return;

        if (IsPlateFull(food.NutritionValue))
            return;

        // var render = food.GetComponent<SpriteRenderer>();
        // transform.GetChild(Foods.Count).GetComponent<SpriteRenderer>().sprite = render.sprite;

        // Ngisi nilai makanan yang ada di piring
        plateValue[food.NutritionValue] += food.FoodValue;

        if (food.NutritionValue == Nutrition.Karbohidrat)
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
        var newFood = Instantiate(food, placePoint.position, Quaternion.identity, foodsParent);
        newFood.GetComponent<CircleCollider2D>().enabled = false;
        // newFood.GetComponent<Food>().enabled = false;

        // Destroy(newFood.GetComponent<CircleCollider2D>());
        // Destroy(newFood.GetComponent<Food>());

        // add makanan ke list
        Foods.Add(food);
    }

    private bool IsPlateFull(Nutrition val)
    {
        if (karbo == 0 && protein_serat == 0 && mineral_kalsium == 0)
            return false;

        if (val == Nutrition.Karbohidrat && karbo == 1)
            return true;

        if ((val == Nutrition.Protein || val == Nutrition.Serat) && protein_serat == 2)
            return true;

        if ((val == Nutrition.Mineral || val == Nutrition.Kalsium) && mineral_kalsium == 2)
            return true;

        return false;
    }

    public void ResetPlate()
    {
        plateValue[Nutrition.Karbohidrat] = 0;
        plateValue[Nutrition.Protein] = 0;
        plateValue[Nutrition.Serat] = 0;
        plateValue[Nutrition.Mineral] = 0;
        plateValue[Nutrition.Kalsium] = 0;

        karbo = 0;
        protein_serat = 0;
        mineral_kalsium = 0;

        Foods.Clear();

        for (int i = 0; i < foodsParent.childCount; i++)
        {
            Destroy(foodsParent.GetChild(i).gameObject);
        }
    }
}
