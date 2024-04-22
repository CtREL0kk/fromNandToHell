using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _helth = 100;
    //[SerializeField] private ParticleSystem _effect;
    
    public void TakeDamage(int damage)
    {
        _helth -= damage;
        if(_helth <= 0)
        {
            //Instantiate(_effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    
    void Update()
    {
        
    }
}
