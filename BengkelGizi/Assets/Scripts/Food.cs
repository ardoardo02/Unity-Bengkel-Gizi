using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    // [SerializeField] AudioManager audioManager;
    [SerializeField] FoodTray foodTray;
    [SerializeField] Renderer foodRenderer;
    [SerializeField] Renderer plateRenderer;
    [SerializeField] Transform foodsRenderParent;
    [SerializeField] TMP_Text nutritionText;

    [Header("Food Property")]
    // [SerializeField] string foodName;
    [SerializeField] Nutrition nutritionValue;
    [SerializeField] int foodValue = 1;

    // public string FoodName { get => foodName; }
    public Nutrition NutritionValue { get => nutritionValue; }
    public int FoodValue { get => foodValue; }

    private void Start()
    {
        nutritionText.text = nutritionValue + ": " + foodValue.ToString();
    }

    private void OnMouseDown()
    {
        ChangeFoodsColor(Color.white);

        foodTray.AddFood(this, transform.GetChild(0));
    }

    private void OnMouseUp()
    {
        ChangeFoodsColor(new Color(0.8f, 0.8f, 0.8f));
    }

    private void OnMouseEnter()
    {
        ChangeFoodsColor(new Color(0.8f, 0.8f, 0.8f));
    }

    private void OnMouseExit()
    {
        ChangeFoodsColor(Color.white);
    }

    private void ChangeFoodsColor(Color color)
    {
        foodRenderer.material.color = color;
        plateRenderer.material.color = color;

        foreach (Transform food in foodsRenderParent)
        {
            food.GetComponent<Renderer>().material.color = color;
        }
    }
}
