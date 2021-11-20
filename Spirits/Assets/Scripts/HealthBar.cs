using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public RawImage healthBarImage;
    public Rect uvRect;

    public RectTransform healthEdge;

    public RectTransform healthBarMask;
    private Vector2 maskDelta;
    private float maskWidth = 240f;

    public Transform player;
    private float playerMaxHealth;

    void Start()
    {
        playerMaxHealth = player.GetComponent<Player_Combat>().maxHealth;
        maskWidth = healthBarMask.rect.width;
    }

    void Update()
    {
        uvRect = healthBarImage.uvRect;
        uvRect.x -= 0.33f * Time.deltaTime / (player.GetComponent<Player_Combat>().health / playerMaxHealth);
        healthBarImage.uvRect = uvRect;
    }

    public void UpdateHealthBar(int dmg) 
    {
        maskDelta = new Vector2(dmg / playerMaxHealth, 0) * maskWidth;
        healthBarMask.offsetMax -= maskDelta;

        healthEdge.anchoredPosition -= maskDelta;
    }
}