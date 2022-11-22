using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    enum Nutrition
    {
        Karbohidrat,
        Protein,
        Serat,
        Mineral,
        Kalsium
    }

    [SerializeField] FoodTray foodTray;

    [Header("Food Property")]
    [SerializeField] string foodName;
    [SerializeField] Nutrition nutrition;
    [SerializeField] int value = 1;

    Color originalColor;
    Renderer render;

    public string FoodName { get => foodName; }

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
        Debug.Log(nutrition);
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
