using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public AIDestinationSetter setter;
    public AIPath path;
    public BoxCollider2D hitbox;
    public GameObject[] avoid;
    public GameObject moveRunAway;
    public Rigidbody2D rb;
    public bool run = false;
	public int maxHealth = 100;
    public int currentHealth;
    public int RunAwaySpeed = 4;
    public int RunAwayDistance = 5;
	public int attackDist = 3;
    public int attackDamageShort = 5;
    public EnemyHealthBar healthBar;
    public int ghostType = 0;
    public float attackRate = 2f;

    bool isFainted = false;
    float nextAttackTime = 0f;

     public void HitBoxOn(){
        Collider2D curr = hitbox;
        curr.enabled = true;
    }

    public void HitBoxOff(){
        Collider2D curr = hitbox;
        curr.enabled = false;
    }

    // Start is called before the first frame update
    public void animateHealth(){
        animator.SetInteger("Health", currentHealth);
    }

    public void animateDamage(){
        animator.ResetTrigger("Damage");
        animator.SetTrigger("Damage");        
    }

    public void animateDeath(){
        animator.ResetTrigger("Death");
        animator.SetTrigger("Death");        
    }

    public void animateAttack(){
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    public void animateSpeed(){
        //float speed = GetComponent<Rigidbody2D>().velocity.magnitude; 
        // Debug.Log(speed);
        if (!path.reachedEndOfPath)
            animator.SetFloat("Speed", 10);
        else
            animator.SetFloat("Speed", 0);
    }

    void Start()
    {
        player = GameObject.Find("Bartender").transform;
        path = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        animateHealth();
        rb = GetComponent<Rigidbody2D>();
        if (avoid == null)
            avoid = GameObject.FindGameObjectsWithTag("ObjectInWorld");
    }

    void Update(){
        if(!isFainted){
            Vector3 vel = Vector3.Normalize(path.velocity);
            if (vel != Vector3.zero){
                //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, vel);
                if (vel.x > 0)
                    transform.rotation = new Quaternion(0, 0, 0, 0);//Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.deltaTime);
                else
                    transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            animateSpeed();
            attackPlayer();
            if (currentHealth < 30) run = true;
            moveAway();
        }
        
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
    	currentHealth -= damage;
        healthBar.UpdateHealthBar();
    	// play hurt animation
    	if(currentHealth <= 0)
        {
    		Die();
    	}
        else{
            animateDamage();
        }
    }

    public bool IsFainted(){
        return isFainted;
    }

    void moveAway(){
        if (!run){
            setter.target = player;    
            return;   
        }
        else{
            if (Vector2.Distance(transform.position, player.position) < RunAwayDistance){
                transform.position = Vector2.MoveTowards(transform.position, player.position, -1 * RunAwaySpeed * Time.deltaTime);
                setter.target = transform;
            }
        }
    }

    void attackPlayer()
    {
        //Debug.Log(Vector3.Dot(transform.right, transform.position - player.position));
        if (-1 * Vector3.Dot(transform.right, transform.position - player.position) > (0.1f)) //transform.right is the direction it's looking, if dot product is > 0 the player is in front of the enemy
        {
        //do whatever
            if (Time.time >= nextAttackTime)
            {
                if (Vector2.Distance(transform.position, player.position) > attackDist) return;
                animateAttack();
                // player.GetComponent<Player_Combat>().TakeDamage(attackDamageShort);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        } 
    }

    void Faint(){
        isFainted = true;
        setter.target = transform;
        foreach(var c in gameObject.GetComponentsInChildren<Collider2D>()){
            c.isTrigger = true;
        }
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
        animateDeath();
        Destroy(gameObject);
    }

    public void Captured(){
        Destroy(gameObject);
    }

    public int GetGhostType(){
        return ghostType;
    }

    void Die()
    {
        Faint();
    }
}

    


