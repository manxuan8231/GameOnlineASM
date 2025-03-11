using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource musicSource;
    public AudioSource[] sfxSources; // Mảng chứa tất cả các AudioSource của SFX

    private const string MusicPref = "MusicVolume";
    private const string SfxPref = "SfxVolume";

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicPref, 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat(SfxPref, 1f);

        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSfxVolume;

        SetMusicVolume(savedMusicVolume);
        SetSfxVolume(savedSfxVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
        PlayerPrefs.SetFloat(MusicPref, volume);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume)
    {
        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null)
            {
                sfx.volume = volume;
            }
        }
        PlayerPrefs.SetFloat(SfxPref, volume);
        PlayerPrefs.Save();
    }
}