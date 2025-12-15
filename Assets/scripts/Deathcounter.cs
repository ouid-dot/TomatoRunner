using UnityEngine;

using TMPro; // Import TextMeshPro namespace

public class DeathCounter : MonoBehaviour
{
    public static int deathCount = 0; // Static variable to easily access from other scripts
    public TextMeshProUGUI deathText; // Reference to your TextMeshProUGUI component

    void Start()
    {
        // font
        deathText.font = Resources.Load<TMP_FontAsset>("pixel_font");
        deathText.fontSize = 0.6f;
        // Load the death count from PlayerPrefs if it exists, otherwise initialize to 0
        deathCount = PlayerPrefs.GetInt("DeathCount", 0); 
        UpdateDeathText();
    }

    public void IncrementDeathCount()
    {
        deathCount++;
        PlayerPrefs.SetInt("DeathCount", deathCount); // Save the death count
        UpdateDeathText();
    }

    void UpdateDeathText()
    {
        if (deathText != null)
        {
            deathText.text = "Deaths: " + deathCount.ToString();
        }
    }
}