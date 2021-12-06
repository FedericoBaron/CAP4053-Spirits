using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject impactEffect;
    public Rigidbody2D rb;
    public int damage;
    public GameObject objectThatFired = null;
    public float maxTime = 60f; // seconds
    Vector3 dir;
    private void Start(){

        // Vector3 next = transform.position + -transform.right * Time.deltaTime * speed;
        // transform.position = next;
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = -(transform.position - GameObject.Find("Bartender").transform.position) * speed;
        dir = -(transform.position - GameObject.Find("Bartender").transform.position).normalized;
    }

    private void Update(){
         transform.position += dir * speed * Time.deltaTime;
         maxTime -= Time.deltaTime;
         if (maxTime == 0)
             Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){        
        Player_Combat obj = other.gameObject.GetComponent<Player_Combat>(); 
        if (obj != null){
            obj.TakeDamage(damage);
        }
        if (other.tag != "Enemy"){
            Debug.Log(other.name);
            Destroy(gameObject);
            objectThatFired.GetComponent<Enemy>().firingLongRange = false;
            if (impactEffect != null){
               Instantiate(impactEffect, transform.position, Quaternion.identity);
            }
        }
    }

}