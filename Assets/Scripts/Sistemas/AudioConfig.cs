using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    private static readonly string firstPlay = "FirstPlay";
    private static readonly string volumePref = "VolumePref";
    private static readonly string sfxPref = "SfxPref";
    private int firstPlayInt;
    public Slider volumeSlider;
    public Slider sfxSlider;
    private float volumeFloat;
    private float sfxFloat;
    private AudioManager _am;
    // Start is called before the first frame update
    void Start()
    {
        _am = AudioManager.instance;

        firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        if (firstPlayInt == 0)
        {
            volumeFloat = 0.5f;
            volumeSlider.value = volumeFloat;
            PlayerPrefs.SetFloat(volumePref, volumeFloat);

            sfxFloat = 0.5f;
            sfxSlider.value = volumeFloat;
            PlayerPrefs.SetFloat(sfxPref, sfxFloat);

            PlayerPrefs.SetInt(firstPlay, -1);

        }
        else
        {
            volumeFloat = PlayerPrefs.GetFloat(volumePref);
            volumeSlider.value = volumeFloat;

            sfxFloat = PlayerPrefs.GetFloat(sfxPref);
            sfxSlider.value = sfxFloat;
        }
    }


    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
        PlayerPrefs.SetFloat(sfxPref, sfxSlider.value);

    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        foreach (Sound s in _am.sounds)
        {
            s.source.volume = volumeSlider.value;
        }
        SaveSoundSettings();
    }
    public void UpdateSfx()
    {
        foreach (Sound s in _am.sfx)
        {
            s.source.volume = volumeSlider.value;
        }
        SaveSoundSettings();
    }
}
