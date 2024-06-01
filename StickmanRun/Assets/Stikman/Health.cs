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
            playerCamera.transform.parent = null;
            //DisableHead();
            gameObject.SetActive(false);
        }
    }

    private void DisableHead()
    {
        for (var i = 0; i < transform.childCount; i++) 
        {
            var child = transform.GetChild(i);
            if (child != null && child.name == "������")
            {
                child.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}
