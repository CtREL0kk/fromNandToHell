using System.Collections;
using UnityEngine;


public class Raycastshoot : MonoBehaviour
{
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private int _damage = 40;
    [SerializeField] private Transform _spavnPoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _damageableLayers;
    private GameObject pauseWindow;
    private GameObject settingsWindow;
    private AudioSource soundsAudioSource;
    private AudioClip shootSound;


    public void Start()
    {
        soundsAudioSource = GameObject.FindWithTag("SoundsAudioSource").GetComponent<AudioSource>();
        shootSound = Resources.Load<AudioClip>("Audio/Sounds/Shoot");
        var canvas = GameObject.FindWithTag("CanvasUI");
        pauseWindow = canvas.transform.GetChild(0).gameObject;
        settingsWindow = canvas.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (pauseWindow.activeSelf || settingsWindow.activeSelf) return;
        RotateGun();
        
        if (Input.GetMouseButtonDown(0))
        {
            soundsAudioSource.PlayOneShot(shootSound);
            StartCoroutine(Raycast(_lineRenderer, _spavnPoint, _damage, _spavnPoint.right, _damageableLayers));
        }      
    }

    private void RotateGun()
    {
        var mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(mouseCoordinates.y, mouseCoordinates.x) * Mathf.Rad2Deg;
        
        if (angle >= -90f && angle <= 90f)
        {
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, 
                Quaternion.Euler(0, 0, angle), Time.deltaTime * 10);
        }    
    }


    private static IEnumerator Raycast(LineRenderer lineRenderer, Transform spawnPoint, int damage, Vector3 direction, LayerMask damageableLayers)
    {
        var hitInfo = Physics2D.Raycast(spawnPoint.position, direction, Mathf.Infinity, damageableLayers);
        if (hitInfo)
        {
            var damageableObject = hitInfo.transform.GetComponent<IDamageable>();
            damageableObject?.TakeDamage(damage);

            lineRenderer.SetPosition(0, spawnPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, spawnPoint.position);
            lineRenderer.SetPosition(1, spawnPoint.position + direction * 100);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
