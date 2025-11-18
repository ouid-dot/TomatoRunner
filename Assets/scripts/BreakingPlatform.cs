using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakingPlatform : MonoBehaviour
{
    private Collider2D platCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] float animTime = 1;
    [SerializeField] float respawnTime = 3;

    private void Start()
    {
        platCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine("BreakPlatform");
        }
        
    }

    IEnumerator BreakPlatform()
    {
        animator.SetBool("break", true);
        animator.SetBool("respawn", false);
        yield return new WaitForSeconds(animTime);
        Components(false);

        // wait a few seconds before respawning
        Invoke("RespawnPlatform", respawnTime);
    }

    private void Components(bool state)
    {
        spriteRenderer.enabled = state;
        platCollider.enabled = state;
        animator.SetBool("break", false);
        animator.SetBool("respawn", false);
    }

    private void RespawnPlatform()
    {
        // play respawn animation
        spriteRenderer.enabled = true;
        animator.SetBool("break", false);
        animator.SetBool("respawn", true);
        Invoke("EnableComponents", 0.5f);
    }

    private void EnableComponents()
    {
        platCollider.enabled = true;
        animator.SetBool("break", false);
        animator.SetBool("respawn", false);
    }

}
