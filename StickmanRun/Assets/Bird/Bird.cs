using System.Collections;
using System.Linq;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float birdSpeed = 2f;
    [SerializeField] private float yThreshold = 10f;
    [SerializeField] private CircleCollider2D triggerZone;
    private bool isFlying;
    private Vector3 startPosition;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition= transform.position;
    }


    private void Update()
    {
        if (!isFlying && PlayerInArea())
            StartFlying();
        
        if (isFlying && transform.position.y - startPosition.y > yThreshold)
        {
            Destroy(gameObject);
        }
    }

    private bool PlayerInArea()
    {
        var triggerZoneCenter = triggerZone.bounds.center;
        var colliders = Physics2D.OverlapCircleAll(triggerZoneCenter, triggerZone.radius);
        return colliders.Any(collider => collider.CompareTag("Player"));
    }

    public void StartFlying()
    {
        Debug.Log("Start Fly");
        isFlying = true;
        StartCoroutine(FlyRandomly());
        animator.SetBool("IsFlying", true);
    }

    private IEnumerator FlyRandomly()
    {
        while (isFlying)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(0.5f, 1.0f) * (Random.value > 0.5f ? 1 : -1),
                Random.Range(0.5f, 1.0f),
                0f
            ).normalized;

            var moveDuration = Random.Range(1.0f, 2.0f);

            for (var t = 0f; t < moveDuration; t += Time.deltaTime)
            {
                transform.Translate(randomDirection * birdSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}