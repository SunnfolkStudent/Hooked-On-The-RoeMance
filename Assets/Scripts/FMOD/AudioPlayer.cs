using System;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;
    
    public void PlayBite()
    {
        RuntimeManager.PlayOneShot("event:/SFX/sfx_fishingBite");
    }

    public void PlayReel()
    {
        RuntimeManager.PlayOneShot("event:/SFX/sfx_fishingReel");
    }

    public void FishThrow()
    {
        RuntimeManager.PlayOneShot("event:/SFX/sfx_fishingThrow");
    }
}
