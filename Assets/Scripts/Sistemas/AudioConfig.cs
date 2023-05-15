using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    private static readonly string firstPlay = "FirstPlay";
    private static readonly string volumePref = "VolumePref";
    private int firstPlayInt;
    public Slider volumeSlider;
    private float volumeFloat;
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
            PlayerPrefs.SetInt(firstPlay, -1);
        }
        else
        {
            volumeFloat = PlayerPrefs.GetFloat(volumePref);
            volumeSlider.value = volumeFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
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
}
