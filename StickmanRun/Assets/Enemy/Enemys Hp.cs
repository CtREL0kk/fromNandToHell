using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysHp : MonoBehaviour, IDamageable
{
    [SerializeField] int hp;
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
