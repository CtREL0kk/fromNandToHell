using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyIndicatorManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject indicatorPrefab;

    private EnemyTrigger enemyTrigger;
    private Dictionary<Collider2D, GameObject> enemyIndicators = new Dictionary<Collider2D, GameObject>();

    void Start()
    {
        enemyTrigger = FindObjectOfType<EnemyTrigger>();
    }

    void Update()
    {
        var enemiesInRange = enemyTrigger.GetEnemiesInRange();

        AddNewIndicators(enemiesInRange);
        RemoveIndicators(enemiesInRange);
    }

    private void AddNewIndicators(List<Collider2D> enemiesInRange)
    {
        foreach (var enemy in enemiesInRange)
        {
            if (!enemyIndicators.ContainsKey(enemy))
            {
                GameObject indicator = Instantiate(indicatorPrefab, transform);
                indicator.GetComponent<EnemyIndicator>().Initialize(mainCamera, enemy);
                enemyIndicators.Add(enemy, indicator);
            }
        }
    }

    private void RemoveIndicators(List<Collider2D> enemiesInRange)
    {
        var enemiesToRemove = new List<Collider2D>();
        foreach (var entry in enemyIndicators)
        {
            if (!enemiesInRange.Contains(entry.Key))
            {
                Destroy(entry.Value);
                enemiesToRemove.Add(entry.Key);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            enemyIndicators.Remove(enemy);
        }
    }

}
