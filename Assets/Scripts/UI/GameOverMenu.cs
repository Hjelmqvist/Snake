using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] int _mainMenuSceneIndex = 0;

    public void RestartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    public void ExitGame()
    {
        SceneManager.LoadScene( _mainMenuSceneIndex );
    }
}