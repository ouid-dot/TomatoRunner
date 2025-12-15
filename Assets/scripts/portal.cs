using UnityEngine;
using UnityEngine.SceneManagement; // Important for scene management

public class FinishLine : MonoBehaviour
{
    private AudioManager audioManager;
    // Optionally, you can make this public to set in the Inspector
    public int nextLevelIndex;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            audioManager.PlaySFX(audioManager.portalIn);
        }
    }
    private void Awake()
    {
        // save current scene
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
}
