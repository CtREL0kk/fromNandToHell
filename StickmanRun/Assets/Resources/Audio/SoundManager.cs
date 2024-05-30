using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource audioSrc;

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        audioSrc.PlayOneShot(clip, volume);
    }
}
