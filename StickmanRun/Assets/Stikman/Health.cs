using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    [SerializeField] private Camera playerCamera;
    private GameObject deathWindow;
    private PostProcessVolume postProcess;
    private AudioSource musicAudioSource;

    public void Start()
    {
        musicAudioSource = GameObject.FindWithTag("MusicAudioSource").GetComponent<AudioSource>();
        postProcess = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
        var canvas = GameObject.FindWithTag("CanvasUI");
        deathWindow = canvas.transform.GetChild(2).gameObject;
    }
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            musicAudioSource.Pause();
            postProcess.enabled = false;
            deathWindow.SetActive(true);
            playerCamera.transform.parent = null;
            //DisableHead();
            gameObject.SetActive(false);
        }
    }

    private void DisableHead()
    {
        for (var i = 0; i < transform.childCount; i++) 
        {
            var child = transform.GetChild(i);
            if (child != null && child.name == "������")
            {
                child.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}
