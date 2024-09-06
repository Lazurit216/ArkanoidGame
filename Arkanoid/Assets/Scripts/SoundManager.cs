using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
