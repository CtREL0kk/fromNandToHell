using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    private AudioSource soundsAudioSource;
    private AudioClip buttonSound;

   public void Play()
   {
        soundsAudioSource.PlayOneShot(buttonSound);
   }

    public void Start()
    {
        soundsAudioSource = GameObject.FindWithTag("SoundsAudioSource").GetComponent<AudioSource>();
        buttonSound = Resources.Load<AudioClip>("Audio/Sounds/Button");
    }
}
