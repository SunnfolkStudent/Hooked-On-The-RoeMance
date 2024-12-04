using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    [Header("Components")] 
    private InputActions _input;

    private void Start()
    {
        _input = GetComponent<InputActions>();
    }
    
    private void Update()
    {
        if (_input.interact)
        {
            SceneManager.LoadScene(0);
        }
    }
}
