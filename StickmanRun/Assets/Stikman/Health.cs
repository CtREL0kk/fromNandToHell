using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 100;
    [SerializeField] Camera playerCamera;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            playerCamera.transform.parent = null;
            //DisableHead();
            gameObject.SetActive(false);
            //SceneManager.LoadScene("End scene");
        }
    }

    private void DisableHead()
    {
        for (var i = 0; i < transform.childCount; i++) 
        {
            var child = transform.GetChild(i);
            if (child != null && child.name == "Голова")
            {
                child.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
