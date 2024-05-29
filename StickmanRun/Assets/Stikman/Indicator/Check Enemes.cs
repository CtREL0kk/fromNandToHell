using UnityEngine;
using System.Collections.Generic;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    private List<Collider2D> enemiesInRange = new List<Collider2D>();

    public List<Collider2D> GetEnemiesInRange()
    {
        return enemiesInRange;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            enemiesInRange.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (enemiesInRange.Contains(other))
        {
            enemiesInRange.Remove(other);
        }
    }
}
