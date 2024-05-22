using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    private List<int> highScores = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddHighScore(int score)
    {
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort descending
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(10); // Keep only top 10 scores
        }
    }

    public List<int> GetHighScores()
    {
        return highScores;
    }

     public void OnMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
