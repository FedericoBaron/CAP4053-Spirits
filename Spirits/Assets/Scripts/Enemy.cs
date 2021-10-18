using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public AIDestinationSetter setter;
    
    public GameObject[] avoid;
    public GameObject moveRunAway;
    
    public bool run = false;
	public int maxHealth = 100;
    public int currentHealth;
    public int RunAwaySpeed = 4;
    public int RunAwayDistance = 5;
	public int attackDist = 3;
    public int attackDamageShort = 5;
    
    bool isFainted = false;
    int ghostType = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (avoid == null)
            avoid = GameObject.FindGameObjectsWithTag("ObjectInWorld");
    }

    void Update(){
        attackPlayer();
        if (currentHealth < 30) run = true;
        moveAway();
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

    void attackPlayer(){
        if (Vector2.Distance(transform.position, player.position) > attackDist) return;
        player.GetComponent<Player_Combat>().TakeDamage(attackDamageShort);
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

    // void runAway(){
    //     if (!run){
    //         Transform person = player;
    //         setter.target = person;    
    //         return;   
    //     }
    //     if (Vector2.Distance(transform.position, player.position) > dist) return;
    //     if (moveRunAway == null){
    //         randomSpot();
    //     } 
    //     else{
    //         Destroy(moveRunAway);
    //         moveRunAway = null;
    //         randomSpot();
    //     }
    //     setter.target = moveRunAway.transform;
    // }

    // void OnTriggerEnter2D(Collider2D col){
    //     Debug.Log("Collide");
    //     if (col.name == "Bartender") {
    //         GameObject playObj = col.transform.gameObject;
    //         GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //         attack(col);
    //         Debug.Log("Collide");
    //     }
    // }

    // void OnTriggerStay2D(Collider2D col){
    //     Debug.Log("Collide");
    //     if (col.name == "Bartender") {
    //         GameObject playObj = col.transform.gameObject;
    //         GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //         attack(col);
    //         Debug.Log("Collide1");
    //     }
    // }

    // void attack(Collider2D col){
    //     col.GetComponent<Player_Combat>().TakeDamage(attackDamageShort);
    // }

    // int add(int axis, int dir){
    //     if (axis == 0){
    //         if (dir == 0) return 1;
    //         if (dir == 2) return -1;
    //         return 0;
    //     }
    //     if (dir == 3) return 1;
    //     if (dir == 1) return -1;
    //     return 0;
    // }

    // bool canMove(Vector2 curr){
    //     foreach (GameObject val in avoid){
    //         if (Vector2Int.FloorToInt(curr) == 
    //             Vector2Int.FloorToInt(
    //                 new Vector2(val.transform.position.x, val.transform.position.y)))
    //             return false;
    //     }
    //     return true;
    // }

    // double abs(float number)
    // {
    //     double num = number;           
    //     if(number<0)
    //         num = -1*number;        
    //     return num;
    // }

    // void randomSpot(){
    //     int rep = 100;
    //     bool flag = true;
    //     int dir = 0;
    //     Vector2 next;
    //     do {
    //         Vector2 start = new Vector2(transform.position.x - dist, transform.position.y + dist);
    //         next = start + Vector2.left;
    //         while (next != start){
    //             if (canMove(next)){
    //                 flag = false;
    //                 break;
    //             } 
    //             float dx = next.x;
    //             float dy = next.y;
    //             if ((abs(dx + add(0, dir) - transform.position.x) > dist) || (abs(dy + add(1, dir) - transform.position.y) > dist)){
    //                 dir++;
    //             }
    //             if (dir == 0) dx++;
    //             if (dir == 1) dy--;
    //             if (dir == 2) dx--;
    //             if (dir == 3) dy--; 
    //             next = new Vector2(dx, dy);
    //         }
    //     } while (flag && rep-- > 0);
        
    //     moveRunAway = new GameObject();
    //     moveRunAway.transform.position = new Vector3(next.x, next.y, 0);
    // }


