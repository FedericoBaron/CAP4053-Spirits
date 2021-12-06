using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] clearedLevel;

    void Start()
    {
        
    }

    public int getLevel(string name){
        Debug.Log(name);
        if (name == "Forest") 
            return 0;
        if (name == "Mansion")
            return 1;
        if (name == "ApartmentWBarriers")
            return 2;
        if (name == "Tutorial")
            return 3;
        return -1;
    }

    void spawnSetter(int scaling){
        if(scaling == 0){
            spawn.spawnRate = 20;
        }
        else{
            spawn.spawnRate = spawn.ogSpawnRate / scaling;
        }
        
        Player_Combat.defeated = 0;
        //  Debug.Log("Spawn");
        //  Debug.Log(spawn.max);
        //  Debug.Log(scaling);
        //  Debug.Log(spawn.numberOfEnemies); 
    }

    public void setDifficulty(string name){
        int min = 0;
        
        for (int i = 0; i < clearedLevel.Length; i++){
            if (clearedLevel[i] < clearedLevel[min])
                min = i;
        }
        
        int curr = getLevel(name);
        // Debug.Log(curr);
        // Debug.Log(min);
        if (curr == 3) spawnSetter(0);
        else{
            // Debug.Log(clearedLevel.Length);
            // Debug.Log(clearedLevel[curr]);
            spawnSetter(clearedLevel[curr] - clearedLevel[min] + 1);
        } 
    }
}
