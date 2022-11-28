using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // [SerializeField] AudioManager audioManager;
    [SerializeField] FoodTray foodTray;
    [SerializeField] Renderer foodRenderer;
    [SerializeField] Renderer plateRenderer;

    [Header("Food Property")]
    // [SerializeField] string foodName;
    [SerializeField] Nutrition nutritionValue;
    [SerializeField] int foodValue = 1;

    // public string FoodName { get => foodName; }
    public Nutrition NutritionValue { get => nutritionValue; }
    public int FoodValue { get => foodValue; }

    private void OnMouseDown()
    {
        foodRenderer.material.color = Color.white;
        plateRenderer.material.color = Color.white;
        // audioManager.PlayClickFoodSFX();

        foodTray.AddFood(this, transform.GetChild(0));
    }

    private void OnMouseUp()
    {
        foodRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        plateRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseEnter()
    {
        foodRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        plateRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseExit()
    {
        foodRenderer.material.color = Color.white;
        plateRenderer.material.color = Color.white;
    }
}
