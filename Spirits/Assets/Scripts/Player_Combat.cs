using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_Combat : MonoBehaviour
{
	public Rigidbody2D playerBody;
	public Animator playerAnim;
	public bool currExist = true;
	public Transform attackPoint;
	public float attackRange = 10f;
	public LayerMask enemyLayers;
	public int attackDamageShort = 40;
	public int attackDamageLong = 100;
	public int added = 30;
	public int money = 0;
	public float attackRate = 2f;
	public float health = 100;
	public float maxHealth = 100;
	public int damageTakenCurrently = 0;
	float nextAttackTime = 0f;
	//public PlayerLost GameOverScreen = null;
	List<int> capturedGhosts = new List<int>();

	public GameObject bottle;

	public HealthBar healthBar;

	public Control_List ControlList;

	void Start()
	{   
		ControlList = GetComponent<Control_List>();
		health = maxHealth;
		playerBody = GetComponent<Rigidbody2D>();
		playerAnim = GetComponent<Animator>();
	}

	void Reset(){
		GameObject currHealthBar = GameObject.Find("HealthMask");
		if (healthBar != null)
			healthBar = currHealthBar.GetComponent<HealthBar>();
	}

    // Update is called once per frame
    void Update()
    {
		if (!ControlList.currExist) return;

		if (ControlList.currentTime <= 0){
			GameOver();
		}

		if(Time.time >= nextAttackTime){
			// Short range attack
			if(Input.GetMouseButtonDown(0)){
				playerAnim.SetTrigger("Attacking");
        		Attack_Short();
				nextAttackTime = Time.time + 1f / attackRate;
        	}
			// Throw bottle
			if(Input.GetMouseButtonDown(1))
			{
				StartCoroutine(Attack_Long());
				nextAttackTime = Time.time + 1f / attackRate;
			}
			// Capture ghost
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Capture_Ghost();
			}
		}
    }

	public void GameOver(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    void Attack_Short()
	{
		// add animation 

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

	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject);
	}


	public void TakeDamage(int amt){
		health = 0 > (health - amt) ? 0 : (health - amt);
		damageTakenCurrently += amt;
		healthBar.UpdateHealthBar(amt);
		if (health == 0){
			// Destroy(gameObject);
			GameOver();
			Debug.Log("Player Lost");
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
				int type = enemy.GetComponent<Enemy>().ghostType;
				ControlList.update(ControlList.FindInBank(type), type * 2 + 1);
				money += added;
				MoneyTextManager.instance.setText(added);
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
