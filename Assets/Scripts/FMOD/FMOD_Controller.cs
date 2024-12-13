using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using FMOD;
using Debug = UnityEngine.Debug;

public class FMOD_Controller : MonoBehaviour
{
    FMOD.Studio.EventInstance musicInstance;

    public static FMOD_Controller instance;
    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance("event:/Music/mx_generalMusic");
        musicInstance.start();
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Robin_Test")
        {
            Debug.Log("Robin_Test");
            musicInstance.setParameterByName("Main Music", 1);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager");
        }
        instance = this;
    }

    private void Update()
    {
        
    }

    public void FishingMode(int value)
    {
        musicInstance.setParameterByName("Mode", value);
    }

    public void CharacterTheme(int value)
    {
        musicInstance.setParameterByName("Character", value);
    }

    public void PlayVoiceline(string voiceline)
    {
        RuntimeManager.PlayOneShot(voiceline);
    }
    
}