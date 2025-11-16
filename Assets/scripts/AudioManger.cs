using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFXSource;

[Header("------ Audio clip ------")]    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip portalIn;
    public AudioClip portalOut;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
