using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] AudioSource clickButtonSFX;
    [SerializeField] Sprite activeStarSprite;
    [SerializeField] TMP_Text totalStarsText;
    [SerializeField] Button[] levelButton;
    [SerializeField] int[] starsRequired;

    int totalStars;

    private void Awake()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            int x = i;
            int stars = PlayerPrefs.GetInt(levelButton[x].name + "Stars", 0);

            totalStars += stars;
            totalStarsText.text = totalStars + "/" + (levelButton.Length * 3);

            var starsParent = levelButton[x].transform.Find("Stars");

            if (starsRequired[x] > totalStars)
            {
                levelButton[x].interactable = false;

                starsParent.gameObject.SetActive(false);
                levelButton[x].transform.Find("LevelText").gameObject.SetActive(false);

                var locked = levelButton[x].transform.Find("Locked");
                locked.gameObject.SetActive(true);
                locked.GetComponentInChildren<TMP_Text>().text = starsRequired[x].ToString();

                continue;
            }

            if (x > 0)
            {
                if (PlayerPrefs.GetInt(levelButton[x - 1].name + "Stars", 0) == 0)
                {
                    levelButton[x].interactable = false;
                    starsParent.gameObject.SetActive(false);
                    continue;
                }
            }

            for (int j = 0; j < stars; j++)
            {
                starsParent.GetChild(j).GetComponent<Image>().sprite = activeStarSprite;
            }
            levelButton[x].onClick.AddListener(() => SelectLevel(levelButton[x].name));
        }
    }

    private void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        clickButtonSFX.Play();
    }
}
