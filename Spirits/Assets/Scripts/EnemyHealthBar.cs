using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Transform enemy;

    public void UpdateHealthBar() 
    {
        float enemyHealth = enemy.GetComponent<Enemy>().currentHealth;
        float enemyMaxHealth = enemy.GetComponent<Enemy>().maxHealth;

        float duration = 0.75f * (enemyHealth / enemyMaxHealth);
        healthBarImage.fillAmount = Mathf.Clamp(enemyHealth / enemyMaxHealth, 0, 1f);

        Color newColor = Color.green;
        if (enemyHealth < enemyMaxHealth * 0.25f) 
        {
            newColor = Color.red;
        } 
        else if (enemyHealth < enemyMaxHealth * 0.66f) 
        {
            newColor = new Color(1f, .64f, 0f, 1f);
        }
        healthBarImage.DOColor(newColor, duration);
    }
}
