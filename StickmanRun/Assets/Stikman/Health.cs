using UnityEngine;
using UnityEngine.UI;
public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image WhiteHealthBar;
    [SerializeField] private int hp = 100;
    [SerializeField] private Camera playerCamera;
    
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
        }
    }
}
