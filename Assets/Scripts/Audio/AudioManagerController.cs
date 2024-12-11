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

    private void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/mx_generalMusic");
        musicInstance.start();
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Robin")
        {
            Debug.Log("Scene Robin");
            musicInstance.setParameterByName("Main Music", 1f);
        }
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
