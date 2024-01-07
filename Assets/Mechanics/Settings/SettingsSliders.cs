using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Manages audio settings by adjusting music and SFX volumes through UI sliders.
public class SettingsSliders : MonoBehaviour 
{
    // Reference to the Audio Mixer for controlling audio levels
    public AudioMixer audioMixer;

    // Sets the music volume based on the slider value
    public void SetMusicVolume (float musicvolume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicvolume)*20);
    }

    //// Sets the SFX volume based on the slider value
    public void SetSFXVolume (float sfxvolume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxvolume)*20);
    }
}
