using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource AudioSrc => GetComponent<AudioSource>();

    protected void PlaySound(AudioClip clip, float volume = 1f)
    {
        AudioSrc.PlayOneShot(clip, volume);
       
    }
}
