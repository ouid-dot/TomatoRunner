using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    private AudioManager audioManager;
    private Animator animator;
    private Rigidbody2D rb;
    public DeathCounter deathCounter;

    private void Awake()
    {
        // Get reference to the AudioManager in the scene
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        
    }


    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathCounter = FindFirstObjectByType<DeathCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") && GetComponent<Collider2D>().enabled)
        {
            // Disable the colliders (so only 1 death)
            GetComponent<Collider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            Debug.Log("colliders disabled");

            // To switch to player death animation
            animator.SetBool("die", true);

            // Disable player input
            GetComponent<PlayerJump>().enabled = false;
            GetComponent<Player>().enabled = false;

            // Player death
            Die();
        }
    }

    private void MiniJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 4f);
    }

    private void PlaySoundEffect()
    {
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

    void Die()
    {
        if (deathCounter != null)
        {
            deathCounter.IncrementDeathCount();
        }

        MiniJump();
        PlaySoundEffect();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.4f);
        transform.position = startPos;

        animator.SetBool("die", false);

        GetComponent<PlayerJump>().enabled = true;
        GetComponent<Player>().enabled = true;

        GetComponent<Collider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;

    }
    
}