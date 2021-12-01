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

    public GameObject player;
    private float playerMaxHealth = 100;

    void Start()
    {
        player = GameObject.Find("Bartender"); 
        player.GetComponent<Player_Combat>().healthBar = this;
        playerMaxHealth = player.GetComponent<Player_Combat>().maxHealth;
        maskWidth = healthBarMask.rect.width;
        // healthBarMask.offsetMax = healthBarMask.offsetMax - player.GetComponent<Control_List>().maskDelta;
        // healthEdge.anchoredPosition = healthEdge.anchoredPosition - player.GetComponent<Control_List>().maskDelta;
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
        // player.GetComponent<Control_List>().maskDelta -= maskDelta;
        Debug.Log(maskDelta);
        healthBarMask.offsetMax -= maskDelta;

        healthEdge.anchoredPosition -= maskDelta;
    }
}