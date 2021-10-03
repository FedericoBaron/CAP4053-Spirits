using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

	public Animator animator;

	public Transform attackPoint;
	public float attackRange = 10f;
	public LayerMask enemyLayers;
	public int attackDamage = 40;
	public float attackRate = 2f;
	float nextAttackTime = 0f;



    // Update is called once per frame
    void Update()
    {
		if(Time.time >= nextAttackTime){
			if(Input.GetKeyDown(KeyCode.Space)){
        		Attack();
				nextAttackTime = Time.time + 1f / attackRate;
        	}
		}

        
    }

    void Attack(){
		// add animation 
		// animator.SetTrigger("Attack");

		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		// Debug.Log("we attack" + hitEnemies.length);
		// Damage them 
		// Debug.Log(hitEnemies[0].name);
		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit " + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
		}

    }

    void OnDrawGizmosSelected(){
    	// if(attackPoint == null){
    	// 	Debug.Log("we here");
    	// 	return;
    	// }

    	Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
