using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] int hp;
    [SerializeField] Camera playerCamera;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            playerCamera.transform.parent = null;
            gameObject.SetActive(false);
            SceneManager.LoadScene("End scene");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
