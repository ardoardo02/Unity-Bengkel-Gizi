using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] GameObject orderBox;
    [SerializeField] TMP_Text orderText;
    [SerializeField] Transform hearts;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource talk_SFX;

    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;

    [Header("Customer Property")]
    [SerializeField] float orderTime = 3f;
    [SerializeField] float customerPatience = 25f;
    [SerializeField] int minTotalOrder = 1;
    [SerializeField] int maxTotalOrder = 3;

    private void Awake()
    {
        orderBox.SetActive(false);
        hearts.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Order());
    }

    IEnumerator Order()
    {
        yield return new WaitForSeconds(orderTime);

        anim.SetBool("isTalking", true);
        talk_SFX.Play();
        yield return new WaitForSeconds(0.5f);

        UpdateOrder();
        orderBox.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        anim.SetBool("isTalking", false);
        talk_SFX.Stop();

        StartCoroutine(WaitForOrder());
    }

    IEnumerator WaitForOrder()
    {
        yield return new WaitForSeconds(1f);
        hearts.gameObject.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                yield return new WaitForSeconds(customerPatience / 10f);
                hearts.GetChild(i).GetComponent<SpriteRenderer>().sprite = (j == 0 ? halfHeart : emptyHeart);
            }
        }

        orderBox.SetActive(false);

        anim.SetBool("isTalking", true);
        talk_SFX.pitch = -2.5f;
        talk_SFX.Play();

        yield return new WaitForSeconds(2f);

        anim.SetBool("isTalking", false);
        talk_SFX.Stop();

        yield return new WaitForSeconds(1f);

        Debug.Log("Customer Left");
    }

    public void UpdateOrder()
    {
        int totalOrder = Random.Range(minTotalOrder, maxTotalOrder);

        Debug.Log("Total Order: " + totalOrder);

        // orderText.text = "";
    }
}
