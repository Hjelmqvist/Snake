using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}