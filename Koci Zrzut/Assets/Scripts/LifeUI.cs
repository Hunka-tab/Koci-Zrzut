using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public Image[] fishIcons;     // Przeci¹gnij w Inspectorze 5 ikonek
    public Sprite fishFullSprite; // Ikonka rybki "pe³na"
    public Sprite fishEmptySprite;// Ikonka rybki "pusta"

    public void UpdateLives(int currentLives, int maxLives)
    {
        for (int i = 0; i < fishIcons.Length; i++)
        {
            if (i < currentLives)
                fishIcons[i].sprite = fishFullSprite;
            else
                fishIcons[i].sprite = fishEmptySprite;
        }
    }
}
