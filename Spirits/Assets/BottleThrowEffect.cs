using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrowEffect : MonoBehaviour
{
    // Start is called before the first frame update 
    public int damage;

    private void OnTriggerEnter2D(Collider2D other){        
        Enemy obj = other.GetComponent<Enemy>(); 
        if (obj != null){
            obj.TakeDamage(damage);
        }
    }

}