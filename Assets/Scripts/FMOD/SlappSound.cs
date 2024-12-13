using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SlappSound : MonoBehaviour
{
    public static SlappSound instance;
    
    public void FishSlap(int value)
    {
        RuntimeManager.PlayOneShot("event:/SFX/sfx_slap");
    }
}
