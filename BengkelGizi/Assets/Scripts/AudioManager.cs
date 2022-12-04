using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    [Header("SFX")]
    [SerializeField] AudioSource clickFood_SFX;
    [SerializeField] AudioSource clickFoodtray_SFX;
    [SerializeField] AudioSource resetPlate_SFX;
    [SerializeField] AudioSource plateFull_SFX;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;

        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }

        LoadVolume();
    }

    private void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

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
