using UnityEngine;
using UnityEngine.UI;
public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image WhiteHealthBar;
    [SerializeField] private int hp = 100;
    [SerializeField] private Camera playerCamera;

    private DistanceCounter distanceCounter;
    private HighScoreManager highScoreManager;

    private void Start()
    {
        distanceCounter = GameObject.Find("Random").GetComponent<DistanceCounter>();
        highScoreManager = FindObjectOfType<HighScoreManager>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        HealthBar.fillAmount = hp * 0.01f;
        if (hp <= 0)
        {
            WhiteHealthBar.fillAmount = 0.0f;
            gameObject.GetComponent<RandKeyboard>().ActivateDeathMenu();
            playerCamera.transform.parent = null;
            gameObject.SetActive(false);
            highScoreManager.SaveData((int)distanceCounter.distance);
            Debug.Log("Save: " + (int)distanceCounter.distance);
        }
    }
}
