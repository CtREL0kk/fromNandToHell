using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip buttonSound;

   public void Play()
   {
      audioSource.PlayOneShot(buttonSound);
   }
}
