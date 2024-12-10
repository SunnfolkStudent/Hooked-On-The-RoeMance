using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int CurrentScore;
    void FixedUpdate()
    {
        if (CurrentScore >= 6)
            Victory();
    }
    public void Victory()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
