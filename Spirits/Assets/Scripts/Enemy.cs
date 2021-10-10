using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    
	public int maxHealth = 100;
	int currentHealth;
    bool isFainted = false;
    int ghostType = 0;

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

    public bool IsFainted(){
        return isFainted;
    }

    void Faint(){
        isFainted = true;
        Debug.Log("we faint");
        StartCoroutine(StartFaintTimer());
    }

    public IEnumerator StartFaintTimer(){
        int minimum = 2;
        int maximum = 5;

        float timeRemaining = Random.Range(minimum, maximum);
        
        while(timeRemaining > 0){
            Debug.Log("we at: " + timeRemaining);
            yield return new WaitForSeconds(1.0f);
            timeRemaining--;
        }
        isFainted = false;
        Destroy(gameObject);
        Debug.Log("unfaint");
    }

    public void Captured(){
        Destroy(gameObject);
    }

    public int GetGhostType(){
        return ghostType;
    }

    void Die()
    {
    	Debug.Log("Enemy Died");

        Faint();
    	// Die animation
        // animator.SetBool("IsDead", true);

    	// Disable enemy
        // Destroy(gameObject);
        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;
        
    }
}
