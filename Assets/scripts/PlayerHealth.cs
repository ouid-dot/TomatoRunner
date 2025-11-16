using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHearts = 3;
    private int currentHearts;

    private HeartsUI heartsUI;

    void Start()
    {
        currentHearts = maxHearts;
        heartsUI = FindFirstObjectByType<HeartsUI>();
        if (heartsUI != null)
            heartsUI.UpdateHearts(currentHearts, maxHearts);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        Debug.Log($"TakeDamage called with amount: {amount}");
        currentHearts -= amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        Debug.Log($"Current Hearts after damage: {currentHearts}");

        if (heartsUI != null)
        Debug.Log("Hearts UI updated.");
            heartsUI.UpdateHearts(currentHearts, maxHearts);

        if (currentHearts <= 0)
        {
            Debug.Log("Player health is zero or below, calling Die().");
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        Respawn();
    }

    void Respawn()
    {
        // Reload current scene to respawn player at start
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}