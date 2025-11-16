using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    [Header("Heart Images")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("UI Container")]
    public Image[] hearts;

    public void UpdateHearts(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            // Only show hearts up to maxHealth
            hearts[i].enabled = i < maxHealth;
        }
    }
}