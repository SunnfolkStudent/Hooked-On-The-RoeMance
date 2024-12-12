using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using FMOD;
using Debug = UnityEngine.Debug;

public class AudioManagerController : MonoBehaviour
{
    FMOD.Studio.EventInstance musicInstance;

    public static AudioManagerController instance;
    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance("event:/Music/mx_generalMusic");
        musicInstance.start();
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Erik_Test")
        {
            Debug.Log("Scene Robin");
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
}
