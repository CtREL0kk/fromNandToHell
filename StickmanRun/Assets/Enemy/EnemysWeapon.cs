using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysWeapon : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject trigerZone;
    [SerializeField] private int damage;
    [SerializeField] bool lazer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform spavnPoint;
    [SerializeField] private float delayBtwShoots;
    [SerializeField] private LayerMask damageableLayers;
    [SerializeField] private float angleOfShootDirectionInDeg;

    private float start_time = 0f;
    public bool isPlayerInside => CheckPlayer();
    private Vector3 directionOfShoot => spavnPoint.transform.right.Rotate(angleOfShootDirectionInDeg);

    void Start()
    {
      
    }

    void Update()
    {
        RotateGun();
        Shoot();      
    }

    private void RotateGun()
    {
        var direction = player.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 190;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        if (isPlayerInside && (start_time += Time.deltaTime) > delayBtwShoots)
        {                  
            start_time = 0;
            StartCoroutine(Raycastshoot.Raycast(lineRenderer, spavnPoint, damage, directionOfShoot,  damageableLayers));          
        }       
    }

    
    private bool CheckPlayer()
    {
        var circleCollider = trigerZone.GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            Debug.LogWarning("CircleCollider2D не найден на объекте trigerZone.");
            return false;
        }

        var collidersInArea = Physics2D.OverlapCircleAll(trigerZone.transform.position, circleCollider.radius * trigerZone.transform.localScale.x / 10f);   //формула которую я подбирал час
        foreach (var collider in collidersInArea)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Игрок внутри");
                return true;
            }
        }
        
        return false;
    }
}
