using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] SpriteRenderer orderBox;
    [SerializeField] Transform orderTextParent;
    [SerializeField] Transform hearts;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource talk_SFX;

    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;

    [Header("Customer Property")]
    [SerializeField] float orderTime = 3f;
    [SerializeField] float customerPatience = 25f;
    [SerializeField, Range(1, 5)] int minTotalOrder = 1;
    [SerializeField, Range(1, 5)] int maxTotalOrder = 3;
    [SerializeField, Range(1, 15)] int minNutritionValue = 1;
    [SerializeField, Range(1, 15)] int maxNutritionValue = 10;

    FoodTray foodTray;
    Nutrition nutrition;
    bool isWaitingFood = false;
    int totalOrder;
    int i_heart, j_heart;
    string serveFeedback;
    Coroutine waitForOrderRoutine = null;
    List<Nutrition> nutrtionOrderList = new List<Nutrition>();
    List<Nutrition> nutritionList = new List<Nutrition>();

    Dictionary<Nutrition, int> custOrder = new Dictionary<Nutrition, int>(5){
        {Nutrition.Karbohidrat, 0},
        {Nutrition.Protein, 0},
        {Nutrition.Serat, 0},
        {Nutrition.Vitamin, 0},
        {Nutrition.Kalsium, 0}
    };

    public Dictionary<Nutrition, int> CustOrder { get => custOrder; }

    private void Awake()
    {
        orderBox.gameObject.SetActive(false);
        hearts.gameObject.SetActive(false);

        for (int i = 0; i < orderTextParent.childCount; i++)
        {
            orderTextParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(Order());
        UpdateOrder();
    }

    public void SetFoodTray(FoodTray foodTray)
    {
        this.foodTray = foodTray;
    }

    private void UpdateOrder()
    {
        totalOrder = Random.Range(minTotalOrder, maxTotalOrder + 1);

        while (totalOrder > 0)
        {
            nutrition = (Nutrition)Random.Range(0, 5);
            if (!nutritionList.Contains(nutrition))
            {
                nutritionList.Add(nutrition);

                CustOrder[nutrition] = Random.Range(minNutritionValue, maxNutritionValue + 1);

                var text = orderTextParent.GetChild((int)nutrition);
                text.GetComponent<TMP_Text>().text += CustOrder[nutrition].ToString();
                text.gameObject.SetActive(true);

                totalOrder--;
            }
        }
    }

    IEnumerator Order()
    {
        yield return new WaitForSeconds(orderTime);

        anim.SetBool("isTalking", true);
        talk_SFX.Play();
        yield return new WaitForSeconds(0.5f);

        orderBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        anim.SetBool("isTalking", false);
        talk_SFX.Stop();

        waitForOrderRoutine = StartCoroutine(WaitForOrder());
    }

    IEnumerator WaitForOrder()
    {
        yield return new WaitForSeconds(1f);

        isWaitingFood = true;
        hearts.gameObject.SetActive(true);

        for (i_heart = 0; i_heart < 5; i_heart++)
        {
            if (i_heart > 2 && anim.GetInteger("isAngry") == 0)
                anim.SetInteger("isAngry", 1);
            for (j_heart = 0; j_heart < 2; j_heart++)
            {
                yield return new WaitForSeconds(customerPatience / 10f);
                hearts.GetChild(i_heart).GetComponent<SpriteRenderer>().sprite = (j_heart == 0 ? halfHeart : emptyHeart);
            }
        }

        StartCoroutine(CustomerLeft());
    }

    IEnumerator Eat()
    {
        isWaitingFood = false;
        orderBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isEating", true);
        // anim.Play("Teen1_Eat");
        yield return new WaitForSeconds(Random.Range(4f, 8f));

        anim.SetBool("isEating", false);
        // anim.Play("Teen1_Idle");

        StartCoroutine(CustomerLeft());
    }

    IEnumerator CustomerLeft()
    {
        isWaitingFood = false;
        orderBox.gameObject.SetActive(false);

        if (serveFeedback != "Correct")
        {
            anim.SetInteger("isAngry", 2);
            // talk_SFX.pitch = -2.5f;
            talk_SFX.Play();
        }

        yield return new WaitForSeconds(2f);

        if (serveFeedback != "Correct")
        {
            anim.SetInteger("isAngry", 1);
            talk_SFX.Stop();

            yield return new WaitForSeconds(1f);
        }

        gone();
        Debug.Log("Customer Left");
    }

    public void CutHeart()
    {
        if (i_heart == 4)
        {
            hearts.GetChild(i_heart).GetComponent<SpriteRenderer>().sprite = emptyHeart;
            StopCoroutine(waitForOrderRoutine);
            StartCoroutine(CustomerLeft());
            return;
        }

        if (j_heart == 0)
        {
            hearts.GetChild(i_heart).GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else
        {
            hearts.GetChild(i_heart).GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts.GetChild(i_heart + 1).GetComponent<SpriteRenderer>().sprite = halfHeart;
        }

        i_heart++;

        if (i_heart > 2 && anim.GetInteger("isAngry") == 0)
            anim.SetInteger("isAngry", 1);
    }

    private void gone()
    {
        if (serveFeedback == "Correct")
            GameManager.Instance.CustServe();
        else
            GameManager.Instance.CustGone();

        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (isWaitingFood)
        {
            serveFeedback = foodTray.ServeFood(this);

            if (serveFeedback == "Not Serving")
                return;

            if (serveFeedback == "Correct")
            {
                StopCoroutine(waitForOrderRoutine);
                StartCoroutine(Eat());
            }
            else if (serveFeedback == "Wrong")
            {
                CutHeart();
            }
            // foodTray.TerimaPlate();
        }

        // Debug.Log("Customer Served");
        // GameManager.Instance.CustServe();
        // Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        if (!foodTray.IsServing || !isWaitingFood)
            return;

        orderBox.material.color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseExit()
    {
        orderBox.material.color = Color.white;
    }
}
