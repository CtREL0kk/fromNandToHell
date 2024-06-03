using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private int highScore = 0;

    public void SaveData(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            Debug.Log("SAve in maneger: " + score);
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    public int LoadData()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        Debug.Log("Load in maneger: " + highScore);
        return highScore;
    }
}
