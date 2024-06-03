using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    [SerializeField] private float birdSpeed = 2f;
    [SerializeField] private float yThreshold = 10f;
    [SerializeField] private CircleCollider2D triggerZone;
    private bool isFlying;
    private Vector3 startPosition;
    private Animator animator;
    private bool isPlayerInArea;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition= transform.position;
    }


    private void Update()
    {
        if (!isFlying && isPlayerInArea)
            StartFlying();
        
        if (isFlying && transform.position.y - startPosition.y > yThreshold) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerInArea = true;
    }
    

    private void StartFlying()
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