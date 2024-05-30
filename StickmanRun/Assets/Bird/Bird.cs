using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float birdSpeed = 2f;
    [SerializeField] private float yThreshold = 10f;
    [SerializeField] private CircleCollider2D trigerZone;
    private bool isFlying = false;
    private Vector3 startPosition;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition= transform.position;
    }


    private void Update()
    {
        if (!isFlying && EnemysWeapon.CheckPlayer(trigerZone))
            StartFlying();
        
        if (isFlying && transform.position.y - startPosition.y > yThreshold)
        {
            Destroy(gameObject);
        }
    }

    public void StartFlying()
    {
        isFlying = true;
        StartCoroutine(FlyRandomly());
        animator.SetBool("IsFlying", true);
    }

    private IEnumerator FlyRandomly()
    {
        while (isFlying)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(0.5f, 1.0f) * (Random.value > 0.5f ? 1 : -1), // ��������� �������� �� X
                Random.Range(0.5f, 1.0f), // �������� �� Y ������ �������������
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