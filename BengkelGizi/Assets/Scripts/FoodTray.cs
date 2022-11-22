using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    public List<Food> Foods = new List<Food>();

    public void AddFood(Food food)
    {
        if (Foods.Contains(food) || Foods.Count >= 3)
            return;

        // var render = food.GetComponent<SpriteRenderer>();
        // transform.GetChild(Foods.Count).GetComponent<SpriteRenderer>().sprite = render.sprite;

        Debug.Log(Foods.Count);
        var parent = transform.GetChild(1).GetChild(Foods.Count);

        var newFood = Instantiate(food, parent.position, Quaternion.identity, parent);
        newFood.GetComponent<CircleCollider2D>().enabled = false;
        // newFood.GetComponent<Food>().enabled = false;

        // Destroy(newFood.GetComponent<CircleCollider2D>());
        // Destroy(newFood.GetComponent<Food>());

        Foods.Add(food);
    }
}
