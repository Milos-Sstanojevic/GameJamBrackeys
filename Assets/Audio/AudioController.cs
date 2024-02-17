using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.Burst.Intrinsics.X86.Avx;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public void SetMaster(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("MasterVolume", db);
        }
    }
    public void SetMusic(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("MusicVolume", db);
        }
    }
    public void SetSoundFX(float percentage)
    {
        if (mixer != null)
        {
            float db = (1f - percentage) * -80f;
            mixer.SetFloat("SoundFXVolume", db);
        }
    }
}
