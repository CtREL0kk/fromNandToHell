using UnityEngine;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 100;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject DeathWindow;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            DeathWindow.SetActive(true);
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
