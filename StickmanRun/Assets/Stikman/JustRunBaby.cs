using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float forceJump = 40;
    [SerializeField] private BoxCollider2D checkGround;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip tackleSound;
    [SerializeField] private AudioSource audioSource;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    private BoxCollider2D cl;
    private bool isDead = false;
    private bool _isGrounded => 
        Physics2D.OverlapArea(checkGround.bounds.min, checkGround.bounds.max, LayerMask.GetMask("Ground"));

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;      
        HorizontalMove();
        VerticalMove();
    }

    private void HorizontalMove()
    {
        var value = moveSpeed * Time.deltaTime;
        transform.Translate(value * Vector3.right);
    }

    private void VerticalMove()
    {
        Jump();
        Tackle();
    }

    private void Tackle()
    {
        if (Input.GetKeyDown(KeyCode.S) && _isGrounded)
        {
            audioSource.PlayOneShot(tackleSound);
            an.SetTrigger("Tackle");
        }
        else an.ResetTrigger("Tackle");
    }
    
    private void Jump()
    {
        if (!_isGrounded && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            rb.AddForce(-Vector3.up * forceJump);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _isGrounded && !an.GetCurrentAnimatorStateInfo(0).IsName("Tackle"))
        {
            rb.AddForce(Vector3.up * forceJump, ForceMode2D.Impulse);
            an.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
        }
        else
            an.ResetTrigger("Jump");
    }

    [SerializeField] private Transform checkWallPoint;
    [SerializeField] private float distanceToDeathFromPlatform = -30;
    private float? saveWallHeight = null; 
    private void FixedUpdate()
    {
        if (isDead)
            return;
        
        var pos = transform.position;
        if (saveWallHeight is not null && pos.y < saveWallHeight - distanceToDeathFromPlatform)
        {
            KillStickman();
            isDead = true; 
        }

        var wallOrigin = checkWallPoint.transform.position;
        var wallDir = Vector2.right;
        var wallHit = Physics2D.Raycast(wallOrigin, wallDir, moveSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Ground"));
        if (wallHit.collider != null)
        {
            if (wallHit.collider.CompareTag("Obstacle"))
            {
                HandleObstacle();
            }
            else
            {
                Fall(wallHit);
            }
           
        }
    }

    [SerializeField] private GameObject whiteSquare;
    [SerializeField] private float fadeDuration = 3.0f;
    private GameObject whiteSquareInstance;

    private void HandleObstacle()
    {
        if (whiteSquareInstance == null)
        {
            whiteSquareInstance = Instantiate(whiteSquare, checkWallPoint.transform.position, Quaternion.identity);
            whiteSquareInstance.transform.position = whiteSquareInstance.transform.position + new Vector3(0, 0, -2);
            moveSpeed = 0;
            StartShakeCamera();
            StartCoroutine(FadeOutCoroutine());
        }
    }

    [SerializeField] CameraShake cameraShake;
    private void StartShakeCamera() => cameraShake.ShakeCamera(fadeDuration);
    
    IEnumerator FadeOutCoroutine()
    {
        if (whiteSquareInstance == null)
            yield break; 

        SpriteRenderer squareRenderer = whiteSquareInstance.GetComponent<SpriteRenderer>();
        var startAlpha = squareRenderer.color.a;
        var startTime = Time.time;

        while (Time.time - startTime < fadeDuration)
        {
            var timePassed = Time.time - startTime;
            var alpha = Mathf.Lerp(startAlpha, 0f, timePassed / fadeDuration);

            var newColor = squareRenderer.color;
            newColor.a = alpha;
            squareRenderer.color = newColor;

            yield return null;
        }

        Destroy(whiteSquareInstance);
        whiteSquareInstance = null;
        KillStickman();
    }

    private void Fall(RaycastHit2D wallHit)
    {
        var boxCollider = wallHit.collider.GetComponent<BoxCollider2D>();
        if (boxCollider != null && transform.position.y < boxCollider.bounds.size.y)
        {
            saveWallHeight = boxCollider.bounds.size.y;
            moveSpeed = 0;
            DisableHead();
        }
    }
    
    
    private void DisableHead()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child != null && child.name == "Scull")
            {
                child.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    private void KillStickman() => gameObject.transform.GetComponent<IDamageable>().TakeDamage(int.MaxValue);
}
