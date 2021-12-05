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
	// public int added = 30;
	// public int money = 0;
	public int totalMoney = 0;
	public int submitRecipe = 5;
	public int ghostsCaptured = 0;
	public int recipesCompleted = 0;
	public float attackRate = 2f;
	public float health = 100;
	public float maxHealth = 100;
	public int damageTakenCurrently = 0;
	float nextAttackTime = 0f;
	//public PlayerLost GameOverScreen = null;
	int ghostSelect = 0;
	public int[] capturedGhosts = new int[] {0, 0, 0, 0, 0, 0};
	bool activeEffect = true;

	public GameObject bottle;

	public HealthBar healthBar;

	public RectTransform selectIconPos;
	public Image selectIconRGB;
	public GameObject uiInventory;
	public Text[] counts;

	public Control_List ControlList;
	void Start()
	{   
		ControlList = GetComponent<Control_List>();
		health = maxHealth;
		playerBody = GetComponent<Rigidbody2D>();
		playerAnim = GetComponent<Animator>();
		capturedGhosts = new int[] {50, 50, 50, 50, 50, 50};
		Scene sceneCurr = SceneManager.GetActiveScene();
		// current = true;
        // for (int i = 0; i < ControlList.badScenes.Length; i++){
        //     if (sceneCurr.name == ControlList.badScenes[i]){
        //         current = false;
        //         break;
        //     }
        // }
		// if(current){
		selectIconPos = GameObject.Find("Selector").GetComponent<RectTransform>();
		selectIconRGB = GameObject.Find("Selector").GetComponent<Image>();
		uiInventory = GameObject.Find("GhostCount");
		counts = uiInventory.GetComponentsInChildren<Text>();
		// }
	}

	void Reset(){
		GameObject currHealthBar = GameObject.Find("HealthMask");
		if (healthBar != null)
			healthBar = currHealthBar.GetComponent<HealthBar>();
	}

    // Update is called once per frame
    void Update()
    {
		// Scene sceneCurr = SceneManager.GetActiveScene();
		// current = true;
        // for (int i = 0; i < ControlList.badScenes.Length; i++){
        //     if (sceneCurr.name == ControlList.badScenes[i]){
        //         current = false;
        //         break;
        //     }
        // }

		if (!ControlList.currExist) return;

		if (ControlList.currentTime <= 0){
			LevelSummary();
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(ghostSelect == 0)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 0;
				activeEffect = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(ghostSelect == 1)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 1;
				activeEffect = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(ghostSelect == 2)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 2;
				activeEffect = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			if(ghostSelect == 3)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 3;
				activeEffect = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			if(ghostSelect == 4)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 4;
				activeEffect = true;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha6))
		{
			if(ghostSelect == 5)
				activeEffect = !activeEffect;
			else
			{
				ghostSelect = 5;
				activeEffect = true;
			}
		}
		
		// if(current)
		selectIconPos.anchoredPosition = new Vector3(-205 + (35 * ghostSelect), 60, 0);

		if(activeEffect)
			selectIconRGB.color = new Color(selectIconRGB.color.r, selectIconRGB.color.g, selectIconRGB.color.b, 200f);
		else
			selectIconRGB.color = new Color(selectIconRGB.color.r, selectIconRGB.color.g, selectIconRGB.color.b, 0f);


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
				GetComponents<AudioSource>()[1].Play();
			}
		}
    }

	public void GameOver(){
		SceneManager.LoadScene("GameOverScene");
		GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Play();
	}

	public void LevelSummary(){
		SceneManager.LoadScene("LevelSummary");
		GameObject music = GameObject.FindGameObjectWithTag("music");
        if (music != null)
            music.GetComponent<AudioSource>().Play();
	}

    void Attack_Short()
	{
		// add animation 

		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		
		bool paused = PauseMenu.gameIsPaused;

		if (!paused)
		{
			if(hitEnemies.Length != 0)
				GetComponents<AudioSource>()[0].Play();
			else
				GetComponents<AudioSource>()[3].Play();
		}

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
		if(activeEffect)
		{
			if(capturedGhosts[ghostSelect] > 0)
				capturedGhosts[ghostSelect]--;
			counts[ghostSelect].text = capturedGhosts[ghostSelect].ToString();
		}

		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

		Instantiate(bottle, gameObject.transform.position + new Vector3(1f, 1.5f, 0), Quaternion.identity);

		yield return new WaitForSeconds(1.5f);
		GetComponents<AudioSource>()[2].time = 0.25f;
		GetComponents<AudioSource>()[2].Play();
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
				int type = enemy.GetComponent<Enemy>().GetGhostType();
				capturedGhosts[type]++;
				counts[type].text = capturedGhosts[type].ToString();

				enemy.GetComponent<Enemy>().Captured();
				//ControlList.update(ControlList.FindInBank(type), type * 2 + 1);
				// money += added;
				ghostsCaptured += 1;
				// MoneyTextManager.instance.setText(added);
			}
		}

		int index = 0;
		foreach (int typing in capturedGhosts) 
        {
        	Debug.Log("ghosts of type " + index + ": " + capturedGhosts[index]);
			index++;
        }
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
