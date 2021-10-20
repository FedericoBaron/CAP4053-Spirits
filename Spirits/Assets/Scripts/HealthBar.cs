using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Transform player;

    public void UpdateHealthBar() 
    {
        healthBarImage.fillAmount = Mathf.Clamp(player.GetComponent<Player_Combat>().health / player.GetComponent<Player_Combat>().maxHealth, 0, 1f);
    }
}