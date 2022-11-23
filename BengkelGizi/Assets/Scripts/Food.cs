using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] FoodTray foodTray;

    [Header("Food Property")]
    [SerializeField] string foodName;
    [SerializeField] Nutrition nutritionValue;
    [SerializeField] int foodValue = 1;

    Color originalColor;
    Renderer render;

    public string FoodName { get => foodName; }
    public Nutrition NutritionValue { get => nutritionValue; }
    public int FoodValue { get => foodValue; }

    private void Start()
    {
        foodName = gameObject.name;
        render = GetComponent<Renderer>();
        originalColor = render.material.color;
    }

    private void OnMouseDown()
    {
        render.material.color = originalColor;
        foodTray.AddFood(this);
    }

    private void OnMouseEnter()
    {
        render.material.color = Color.green;
    }

    private void OnMouseExit()
    {
        render.material.color = originalColor;
    }
}
