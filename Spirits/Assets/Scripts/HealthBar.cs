using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Transform player;

    public void UpdateHealthBar() 
    {
        float playerHealth = player.GetComponent<Player_Combat>().health;
        float playerMaxHealth = player.GetComponent<Player_Combat>().maxHealth;

        float duration = 0.75f * (playerHealth / playerMaxHealth);
        healthBarImage.fillAmount = Mathf.Clamp(playerHealth / playerMaxHealth, 0, 1f);

        Color newColor = Color.green;
        if (playerHealth < playerMaxHealth * 0.25f) 
        {
            newColor = Color.red;
        } 
        else if (playerHealth < playerMaxHealth * 0.66f) 
        {
            newColor = new Color(1f, .64f, 0f, 1f);
        }
        healthBarImage.DOColor(newColor, duration);
    }
}