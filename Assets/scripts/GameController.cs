using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    private AudioManager audioManager;

    private void Awake()
    {
        // Get reference to the AudioManager in the scene
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        Respawn();

        // Use the instance to play the sound
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.death);
        }
        else
        {
            Debug.LogWarning("AudioManager not found in scene!");
        }
    }

    void Respawn()
    {
        transform.position = startPos;
    }
}