using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
	public Rigidbody2D playerBody;
	public Animator animator;

	public Transform attackPoint;
	public float attackRange = 10f;
	public LayerMask enemyLayers;
	public int attackDamageShort = 40;
	public int attackDamageLong = 100;
	public float attackRate = 2f;
	float nextAttackTime = 0f;
	List<int> capturedGhosts = new List<int>();
	// public Money money;

	public GameObject bottle;

	void Start()
	{
		playerBody = GetComponent<Rigidbody2D>();
		// money = new Money();
	}

    // Update is called once per frame
    void Update()
    {
		if(Time.time >= nextAttackTime){
			// Short range attack
			if(Input.GetKeyDown(KeyCode.Space)){
        		Attack_Short();
				nextAttackTime = Time.time + 1f / attackRate;
        	}
			// Throw bottle
			if(Input.GetKeyDown(KeyCode.B))
			{
				StartCoroutine(Attack_Long());
				nextAttackTime = Time.time + 1f / attackRate;
			}
			// Capture ghost
			if(Input.GetKeyDown(KeyCode.N))
			{
				Capture_Ghost();
			}
		}

        
    }

    void Attack_Short()
	{
		// add animation 
		// animator.SetTrigger("Attack");

		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		// Debug.Log("we attack" + hitEnemies.length);
		// Damage them 
		// Debug.Log(hitEnemies[0].name);
		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit " + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(attackDamageShort);
		}

    }

	IEnumerator Attack_Long()
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

		Instantiate(bottle, gameObject.transform.position + new Vector3(1f, 1.5f, 0), Quaternion.identity);

		yield return new WaitForSeconds(1.5f);
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(mousePos, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies)
		{
			Debug.Log("We hit " + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(attackDamageLong);
		}
	}

	void Capture_Ghost(){
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies){
			if(enemy.GetComponent<Enemy>().IsFainted()){
				capturedGhosts.Add(enemy.GetComponent<Enemy>().GetGhostType());
				enemy.GetComponent<Enemy>().Captured();
				// Money.addCurrMoney(30);
				MoneyTextManager.instance.setText(30);
				// Debug.Log("money is: " + money.getCurrMoney());
			}
		}
		Debug.Log("here are the captured ghosts: " + capturedGhosts);
	}

    void OnDrawGizmosSelected()
	{
    	// if(attackPoint == null){
    	// 	Debug.Log("we here");
    	// 	return;
    	// }

    	Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
