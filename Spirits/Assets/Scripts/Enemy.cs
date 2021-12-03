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
    bool isMoving = false;
    bool isFainted = false;
    public bool animationCurrPlaying = false;
    float nextAttackTime = 0f;

    public void HitBoxOn(){
        Collider2D curr = hitbox;
        curr.enabled = true;
    }
    
    public void AnimationOn(){
       // Debug.Log("entered1");
       animationCurrPlaying = true;
       // Debug.Log(animationCurrPlaying);
    }

    public void AnimationOff(){
       // Debug.Log("entered");
       animationCurrPlaying = false;
       // Debug.Log(animationCurrPlaying); 
    }

    public void HitBoxOff(){
        Collider2D curr = hitbox;
        curr.enabled = false;
    }

    // Start is called before the first frame update

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
        if (animationCurrPlaying) return;

        if (!path.reachedEndOfPath || isMoving)
            animator.SetFloat("Speed", 1);
        else
            animator.SetFloat("Speed", 0);
    }

    void Start()
    {
        player = GameObject.Find("Bartender").transform;
        path = GetComponent<AIPath>();
        setter = GetComponent<AIDestinationSetter>();
        setter.target = player;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        if (avoid == null)
            avoid = GameObject.FindGameObjectsWithTag("ObjectInWorld");
        //initialForward = transform.right; // change this if its obj looking wrong way
    }
    
    // Vector3 initialForward;

    void Update(){
        if(!isFainted){
            if (currentHealth <= 0){
                animateDeath();
                if (animationCurrPlaying) return;
                Die();
            }
            animateSpeed();
            attackPlayer();
            moveAway();
        }
        
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
    	currentHealth -= damage;
        healthBar.UpdateHealthBar();
    	// play hurt animation
        if (currentHealth > 0){
            animateDamage();
        }
    }

    public bool IsFainted(){
        return isFainted;
    }

    void moveAway(){
        if (animationCurrPlaying){
            //setter.target = transform;
            path.isAnimating = true;
            isMoving = false;
            return;    
        }
        path.isAnimating = false;
        if (currentHealth < 30) run = true;
        else run = false;

        if (!run){
            if (setter.target != player)
                setter.target = player;
            else{
                Vector3 vel = Vector3.Normalize(path.velocity);
                if (vel != Vector3.zero){
                    // Flips scale when vel.x < 0
                    Vector3 scale = transform.localScale;
                    if (vel.x < 0) scale.x = -1;
                    else scale.x = 1;
                    transform.localScale = scale;
                    // Rotates enemy around y axis 
                    // Vector3 lookPos = vel; //target.position - transform.position;
                    // Vector3 forward = (transform.position + initialForward) - transform.position;
                    // transform.eulerAngles = new Vector3(0, Vector3.SignedAngle(lookPos, forward, Vector2.down));
                }
                isMoving = false;
            } 
            return;   
        }
        else{
            if (Vector2.Distance(transform.position, player.position) < RunAwayDistance){
                transform.position = Vector2.MoveTowards(transform.position, player.position, -1 * RunAwaySpeed * Time.deltaTime);
                setter.target = transform;
                isMoving = true;
                Vector3 scale = transform.localScale;
                Vector3 vel = (transform.position - player.position);
                if (vel.x < 0) scale.x = -1;
                else scale.x = 1;
                transform.localScale = scale;
            }
            else
                isMoving = false;
            return;
        }
    }

    bool canSeePlayer(){
        // -1 * Vector3.Dot(transform.right, player.position - transform.position) >= (0f);
        if ((player.position - transform.position).x > 0 && transform.localScale.x > 0) return true;
        if ((player.position - transform.position).x < 0 && transform.localScale.x < 0) return true;
        return false;
    }

    void attackPlayer()
    {
        //Debug.Log(Vector3.Dot(transform.right, transform.position - player.position));
        if (canSeePlayer()) //transform.right is the direction it's looking, if dot product is > 0 the player is in front of the enemy
        {
        //do whatever
            if (Time.time >= nextAttackTime)
            {
                if (Vector2.Distance(transform.position, player.position) > attackDist) return;
                animateAttack();
                GetComponent<AudioSource>().Play();
                // player.GetComponent<Player_Combat>().TakeDamage(attackDamageShort);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        } 
    }

    void Faint(){
        isFainted = true;
        setter.target = transform;
        animateDeath();
        foreach(var c in gameObject.GetComponentsInChildren<Collider2D>()){
            c.isTrigger = true;
        }
        StartCoroutine(StartFaintTimer());
    }

    public IEnumerator StartFaintTimer(){
        int minimum = 2;
        int maximum = 5;

        float timeRemaining = Random.Range(minimum, maximum);
        
        //GetComponent<Animator>().enabled = false;
        while(timeRemaining > 0){
           // Debug.Log("we at: " + timeRemaining);
            yield return new WaitForSeconds(1.0f);
            timeRemaining--;
        }
        isFainted = false;

        float reviveProbability = Random.Range(0.0f, 1.0f);
        if(reviveProbability >= 0.05f)
        {
            currentHealth = (int)(reviveProbability * (float)maxHealth);
            healthBar.UpdateHealthBar();
        }
        else
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

    


