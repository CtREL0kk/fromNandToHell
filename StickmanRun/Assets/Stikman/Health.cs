using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

public class Helth : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 100;
    [SerializeField] Camera playerCamera;
    [SerializeField] private GameObject deathWindow;
    [SerializeField] private PostProcessVolume volume;
    [SerializeField] private AudioSource audioSource;
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            audioSource.Pause();
            volume.enabled = false;
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
