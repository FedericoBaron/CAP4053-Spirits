using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrowEffect : MonoBehaviour
{
    // Start is called before the first frame update 
    
    public int attackType = 0;
    
    public int damage = 5;
    public float time = 5f;
    public int increaseHealth = 5;

    void Start(){
        Player_Combat player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        Player_Movement move = GameObject.Find("Bartender").GetComponent<Player_Movement>();

        if (attackType == 2){
            //Debug.Log("health went up");
            player.TakeDamage(-increaseHealth);
        }
        else if (attackType == 5){
            move.SpeedUp(time);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){        
        Enemy obj = other.GetComponent<Enemy>(); 
        
        if (obj != null){
            if (attackType == 0){
                obj.TakeDamage(2 * damage);
            }
            else if (attackType == 1){
                obj.LowerAttack(time);
            }
            else if (attackType == 3){
                obj.takeDamageOverTime(damage, time);
            }
            else if (attackType == 4){
                obj.giveLife(time);
            }
        }
    }

}
