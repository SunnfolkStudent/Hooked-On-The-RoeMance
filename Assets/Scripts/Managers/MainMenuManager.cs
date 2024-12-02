using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //In this script we only need Functions we call when we need.
    //We remove the Start and Update Function seeing as we don't need them. 

    //When this Function is called, it will load the scene called SampleScene.
    //This is the current name of our game scene, so if we change the scene name in the assets folder, we must also change it here. 
    public void GameScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
