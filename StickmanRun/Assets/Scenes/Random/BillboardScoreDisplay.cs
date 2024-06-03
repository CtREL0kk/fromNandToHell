using UnityEngine;


public class BillboardScoreDisplay : MonoBehaviour
{
    public TextMesh scoreText;
    private HighScoreManager highScoreManager;

    void Start()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        int highScore = highScoreManager.LoadData();
        Debug.Log("highScore: " + highScore);
        scoreText.text = highScore.ToString() + " m";
    }
}

