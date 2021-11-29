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
    public Control_List ControlList;
    private float playerMaxHealth = 100;

    void Start()
    {   
        player = GameObject.Find("Bartender").transform;
        Player_Combat combat = player.GetComponent<Player_Combat>();
        playerMaxHealth = combat.maxHealth;
        combat.healthBar = this;
        maskWidth = healthBarMask.rect.width;
        ControlList = player.GetComponent<Control_List>();
        if (ControlList.maskTotal <= 0)
            ControlList.maskTotal = 0;
        //Debug.Log(maskDelta + " " + ControlList.maskTotal);
        maskDelta = new Vector2(ControlList.maskTotal, 0);
        healthBarMask.offsetMax -= maskDelta;
        healthEdge.anchoredPosition -= maskDelta;
    }

    void Update()
    {
        if (healthBarImage == null) return;
        uvRect = (healthBarImage == null ? Rect.zero : healthBarImage.uvRect);
        if (uvRect != null){
            uvRect.x -= 0.33f * Time.deltaTime / (player.GetComponent<Player_Combat>().health / playerMaxHealth);
            healthBarImage.uvRect = uvRect;
        }
    }

    public void UpdateHealthBar(int dmg) 
    {
        if (healthBarMask == null) return;
        if (healthEdge == null) return;
        maskDelta = new Vector2(dmg / playerMaxHealth, 0) * maskWidth;
        Debug.Log(maskDelta);
        healthBarMask.offsetMax -= maskDelta;
        ControlList.maskTotal += dmg / playerMaxHealth * maskWidth;
        healthEdge.anchoredPosition -= maskDelta;
    }
}