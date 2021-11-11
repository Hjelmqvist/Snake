using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
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