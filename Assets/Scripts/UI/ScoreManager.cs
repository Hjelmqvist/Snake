using TMPro;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highscoreText;
    [SerializeField] TextMeshProUGUI _currentScoreText;
    [SerializeField] GameObject _saveScorePanel;
    [SerializeField] TMP_InputField _highscoreNameInput;

    Score _highscore = new Score( "", 0 );
    int _currentPoints;

    const string HIGHSCOREPATH = "highscore.json";

    private void Awake()
    {
        string filepath = GetHighscorePath();
        if (File.Exists( filepath ))
        {
            string json = File.ReadAllText( filepath );
            SetHighscore( JsonUtility.FromJson<Score>( json ) );
        }
    }

    public static string GetHighscorePath()
    {
        return Path.Combine(Application.persistentDataPath, HIGHSCOREPATH);
    }

    public void AddPoints(int points)
    {
        SetCurrentScore(_currentPoints + points);
    }

    /// <summary>
    /// Only opens the panel if the current points are higher than the highscores
    /// </summary>
    public void TryOpenSaveScorePanel()
    {
        if (_currentPoints > _highscore.Points)
            _saveScorePanel.SetActive( true );
    }

    private void SaveNewHighscore(string name)
    {
        SetHighscore( new Score( name, _currentPoints ) );
        File.WriteAllText( GetHighscorePath(), JsonUtility.ToJson( _highscore ) );  

        SetCurrentScore( 0 );
        _saveScorePanel.SetActive( false );
    }

    private void SetCurrentScore(int points)
    {
        _currentPoints = points;
        _currentScoreText.text = $"Score: {_currentPoints}";
    }

    private void SetHighscore(Score score)
    {
        _highscore = score;
        _highscoreText.text = $"{score.Name}: {score.Points}";
    }

    public void SubmitHighscoreName()
    {
        string name = _highscoreNameInput.text;
        if (name.Length != 0)
            SaveNewHighscore( name );
    }
}

public struct Score
{
    public string Name;
    public int Points;

    public Score(string name, int points)
    {
        Name = name;
        Points = points;
    }
}