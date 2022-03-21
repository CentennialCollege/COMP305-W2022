using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource startMenuBGM;
    public AudioMixer startMenuAudioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        startMenuBGM.Play();
        
    }

    public void OnMasterVolumeSlider_Changed()
    {
        var volume = Mathf.Lerp(-40.0f, 0.0f, masterVolumeSlider.value * 0.01f);
        startMenuAudioMixer.SetFloat("MasterVolume", volume);
    }

    public void OnMusicVolumeSlider_Changed()
    {
        var volume = Mathf.Lerp(-40.0f, 0.0f, musicVolumeSlider.value * 0.01f);
        startMenuAudioMixer.SetFloat("MusicVolume", volume);
    }

    public void OnEffectsVolumeSlider_Changed()
    {
        var volume = Mathf.Lerp(-40.0f, 0.0f, effectsVolumeSlider.value * 0.01f);
        startMenuAudioMixer.SetFloat("EffectsVolume", volume);
    }
}
