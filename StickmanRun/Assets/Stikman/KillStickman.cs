using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillStickman : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Helth>();
        if (health != null) 
        {
            Debug.Log("Kill for falling down");
            health.TakeDamage(int.MaxValue);
        }
    }
}
