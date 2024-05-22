using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text highScoreText;

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        List<int> highScores = HighScoreManager.Instance.GetHighScores();
        highScoreText.text = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreText.text += $"{i + 1}. {highScores[i]}\n";
        }
    }
}
