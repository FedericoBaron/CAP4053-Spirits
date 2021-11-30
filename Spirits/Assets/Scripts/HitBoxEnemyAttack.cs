using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemyAttack : MonoBehaviour
{
    public int attackDamageShort = 5;
    // Start is called before the first frame update
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.name == "hitbox" && other.gameObject.name == "Bartender"){
            // Debug.Log(other.gameObject.name + " " + this.gameObject.name);
            other.gameObject.GetComponent<Player_Combat>().TakeDamage(attackDamageShort);
            GetComponents<AudioSource>()[0].Play();
        }
    }
}
