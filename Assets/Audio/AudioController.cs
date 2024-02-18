using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFxSlider;
    private void Start()
    {
        masterSlider.value = 1f - PlayerPrefs.GetFloat("MasterVolume", 0f) / -80f;
        musicSlider.value = 1f - PlayerPrefs.GetFloat("MusicVolume", 0f) / -80f;
        soundFxSlider.value = 1f - PlayerPrefs.GetFloat("SoundFXVolume", 0f) / -80f;

        //mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MasterVolume", 0f));
        //mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
        //mixer.SetFloat("SoundFXVolume", PlayerPrefs.GetFloat("SoundFXVolume", 0f));
    }
    public void SetMaster(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("MasterVolume", db);
            PlayerPrefs.SetFloat("MasterVolume", db);
            PlayerPrefs.Save();
        }
    }
    public void SetMusic(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("MusicVolume", db);
            PlayerPrefs.SetFloat("MusicVolume", db);
            PlayerPrefs.Save();
        }
    }
    public void SetSoundFX(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("SoundFXVolume", db);
            PlayerPrefs.SetFloat("SoundFXVolume", db);
            PlayerPrefs.Save();
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
