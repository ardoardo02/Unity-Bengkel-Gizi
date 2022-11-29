using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] AudioSource clickFood_SFX;
    [SerializeField] AudioSource clickFoodtray_SFX;
    [SerializeField] AudioSource resetPlate_SFX;
    [SerializeField] AudioSource plateFull_SFX;

    public void PlayClickFoodSFX()
    {
        clickFood_SFX.Play();
    }

    public void PlayClickFoodtraySFX()
    {
        clickFoodtray_SFX.Play();
    }

    public void PlayResetPlateSFX()
    {
        resetPlate_SFX.Play();
    }

    public void PlayPlateFullSFX()
    {
        plateFull_SFX.Play();
    }
}
