using UnityEngine;
using UnityEngine.UI;

public class RecordText : MonoBehaviour
{
    [SerializeField] private Text recordText;
    
    public void Start()
    {
        var record = PlayerPrefs.GetInt("HighScore", 0);
        recordText.text = record.ToString();
    }
}
