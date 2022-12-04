using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [Header("Sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [Header("Texts")]
    [SerializeField] TMP_Text masterText;
    [SerializeField] TMP_Text musicText;
    [SerializeField] TMP_Text sfxText;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void OnEnable()
    {
        LoadVolume();
    }

    public void ConfirmChanges()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }
    public void CancelChanges()
    {
        float masterVolume = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    private void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);

        masterText.text = Mathf.RoundToInt(masterVolume * 100) + "%";
        musicText.text = Mathf.RoundToInt(musicVolume * 100) + "%";
        sfxText.text = Mathf.RoundToInt(sfxVolume * 100) + "%";

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    private void SetMasterVolume(float value)
    {
        masterText.text = Mathf.RoundToInt(value * 100) + "%";
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    private void SetMusicVolume(float value)
    {
        musicText.text = Mathf.RoundToInt(value * 100) + "%";
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    private void SetSFXVolume(float value)
    {
        sfxText.text = Mathf.RoundToInt(value * 100) + "%";
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
