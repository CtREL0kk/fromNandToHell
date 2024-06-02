using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RandKeyboard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 45;
    [SerializeField] private float forceJump = 200;
    [SerializeField] private int boost = 2;
    [SerializeField] private BoxCollider2D checkGround;

    private AudioSource musicAudioSource;
    private AudioSource soundsAudioSource;
    private AudioClip jumpSound;
    private AudioClip tackleSound;
    private AudioClip deathSound;

    private GameObject pauseWindow;
    private GameObject settingsWindow;
    private GameObject deathWindow;
    private PostProcessVolume postProcess;

    private DistanceCounter distanceCounter;
    public float CurrentSpeed { get; private set; }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator an;
    private BoxCollider2D cl;
    private bool isDead = false;
    private bool IsGrounded =>
        Physics2D.OverlapArea(checkGround.bounds.min, checkGround.bounds.max, LayerMask.GetMask("Ground"));

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();

        distanceCounter = GameObject.Find("Random").GetComponent<DistanceCounter>();

        postProcess = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
        musicAudioSource = GameObject.FindWithTag("MusicAudioSource").GetComponent<AudioSource>();
        soundsAudioSource = GameObject.FindWithTag("SoundsAudioSource").GetComponent<AudioSource>();
        var canvas = GameObject.FindWithTag("CanvasUI");
        pauseWindow = canvas.transform.GetChild(0).gameObject;
        settingsWindow = canvas.transform.GetChild(1).gameObject;
        deathWindow = canvas.transform.GetChild(2).gameObject;
        deathSound = Resources.Load<AudioClip>("Audio/Sounds/Death");
        jumpSound = Resources.Load<AudioClip>("Audio/Sounds/Jump");
        tackleSound = Resources.Load<AudioClip>("Audio/Sounds/Tackle");
    }

    private void Update()
    {
        if (isDead || pauseWindow.activeSelf || settingsWindow.activeSelf)
            return;
        HorizontalMove();
        VerticalMove();
    }

    private void HorizontalMove()
    {
        var distance = distanceCounter.distance;
        CurrentSpeed = (float)(moveSpeed + distance * 0.1 * boost) * Time.deltaTime ;
        transform.Translate(CurrentSpeed * Vector3.right);
    }

    private void VerticalMove()
    {
        Jump();
        Tackle();
    }

    private void Tackle()
    {
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        {
            soundsAudioSource.PlayOneShot(tackleSound);
            an.SetTrigger("Tackle");
        }
        else an.ResetTrigger("Tackle");
    }

    private void Jump()
    {
        if (!IsGrounded && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            rb.AddForce(-Vector3.up * forceJump);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded && !an.GetCurrentAnimatorStateInfo(0).IsName("Tackle"))
        {
            rb.AddForce(Vector3.up * forceJump, ForceMode2D.Impulse);
            an.SetTrigger("Jump");
            soundsAudioSource.PlayOneShot(jumpSound);
        }
        else
            an.ResetTrigger("Jump");
    }

    [SerializeField] private Transform checkWallPoint;
    [SerializeField] private float distanceToDeathFromPlatform = 30;
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

    private void ActivateDeathMenu()
    {
        soundsAudioSource.PlayOneShot(deathSound);
        musicAudioSource.Pause();
        postProcess.enabled = false;
        deathWindow.SetActive(true);
    }


    [SerializeField] private GameObject whiteSquare;
    [SerializeField] private float fadeDuration = 3.0f;
    private GameObject whiteSquareInstance;

    private void HandleObstacle()
    {
        if (whiteSquareInstance == null)
        {
            ActivateDeathMenu();
            whiteSquareInstance = Instantiate(whiteSquare, checkWallPoint.transform.position, Quaternion.identity);
            moveSpeed = 0;
            StartShakeCamera();
            StartCoroutine(FadeOutCoroutine());
        }
    }

    [SerializeField] private CameraShake cameraShake;
    private void StartShakeCamera() => cameraShake.ShakeCamera(fadeDuration);

    private IEnumerator FadeOutCoroutine()
    {
        if (whiteSquareInstance == null)
            yield break;

        //Debug.Log("Start Corutine");

        var squareRenderer = whiteSquareInstance.GetComponent<SpriteRenderer>();
        var startAlpha = squareRenderer.color.a;
        var startTime = Time.time;
        KillStickman();
        //Debug.Log("Kill Stickman");

        while (Time.time - startTime < fadeDuration)
        {
            //Debug.Log("Cycle");
            var timePassed = Time.time - startTime;
            var alpha = Mathf.Lerp(startAlpha, 0f, timePassed / fadeDuration);

            var newColor = squareRenderer.color;
            newColor.a = alpha;
            squareRenderer.color = newColor;

            yield return null;
        }
        
        Destroy(whiteSquareInstance);
        whiteSquareInstance = null;
    }

    private void Fall(RaycastHit2D wallHit)
    {
        var boxCollider = wallHit.collider.GetComponent<BoxCollider2D>();
        if (boxCollider != null && transform.position.y < boxCollider.bounds.size.y)
        {
            ActivateDeathMenu();
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
