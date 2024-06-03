using UnityEngine;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    [SerializeField] private Camera playerCamera;
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            gameObject.GetComponent<RandKeyboard>().ActivateDeathMenu();
            playerCamera.transform.parent = null;
            gameObject.SetActive(false);
        }
    }
}
