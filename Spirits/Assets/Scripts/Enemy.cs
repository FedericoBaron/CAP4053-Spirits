using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    
	public int maxHealth = 100;
	int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
    	currentHealth -= damage;
        // animator.setTrigger("Hurt");
    	// play hurt animation

    	if(currentHealth <= 0)
        {
    		Die();
    	}
    }

    public void isFainted(){
        
    }

    void Die()
    {
    	Debug.Log("Enemy Died");
    	// Die animation
        // animator.SetBool("IsDead", true);

    	// Disable enemy
        // Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
    }
}
