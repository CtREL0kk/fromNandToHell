using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 1;
    [SerializeField] private GameObject door;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            door.GetComponent<IOpening>().Open();
        }
    }
}
