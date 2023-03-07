using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] SpriteRenderer powerUpRenderer;
    [SerializeField] int powerUpValue = 5; 
    [SerializeField] TMP_Text powerUpText;

    [Header("Etc")]
    [SerializeField] FoodTray foodTray;

    public PowerUpType PowerUpType { get => powerUpType; }

    private void OnMouseDown() {
        if (GameManager.Instance.IsGamePaused || powerUpValue <= 0 || foodTray.IsServing)
            return;

        powerUpRenderer.material.color = Color.white;
        foodTray.IsSelectPowerUp = !foodTray.IsSelectPowerUp;
        foodTray.PowerUpSelected = this;
        ChangeButtonColor();
    }

    private void OnMouseUp() {
        if(GameManager.Instance.IsGamePaused || powerUpValue <= 0 || foodTray.IsServing)
            return;
        
        powerUpRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseEnter() {
        if(GameManager.Instance.IsGamePaused || powerUpValue <= 0 || foodTray.IsServing)
            return;
        
        powerUpRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseExit() {
        if(GameManager.Instance.IsGamePaused || powerUpValue <= 0 || foodTray.IsServing) 
            return;
        
        powerUpRenderer.material.color = Color.white;
    }

    private void ChangeButtonColor()
    {
        if (foodTray.IsSelectPowerUp)
            powerUpRenderer.color = new Color(0.7f, 0.7f, 0.7f);
        else
            powerUpRenderer.color = Color.white;
    }

    public void PowerUpServed()
    {
        powerUpValue--;
        powerUpText.text = powerUpValue.ToString();
        ChangeButtonColor();
    }

    // Start is called before the first frame update
    void Start()
    {
        powerUpText.text = powerUpValue.ToString();
    }
}
