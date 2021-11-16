using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int _playSceneIndex;
    [SerializeField] int _aiPlaySceneIndex;
 
    public void Play() => SceneManager.LoadScene( _playSceneIndex );

    public void AIPlay() => SceneManager.LoadScene( _aiPlaySceneIndex );

    public void Exit() => Application.Quit();
}