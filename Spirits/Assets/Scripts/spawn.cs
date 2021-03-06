using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemy;
    public int[] prob;
    public static int numberOfEnemies = 20;
    public static bool set = false;
    public static int max = 5;
    public int total = 20;
    float randX;
    Vector2 whereToSpawn;
    public static float spawnRate = 20f;
    float nextSpawn = 0.0f;
    public static float ogSpawnRate = 20f;

    void Start()
    {
        total = numberOfEnemies;
    }

    // Update is called once per frame
    void Update()
    {   
        int cnt = 0;
        if (Time.time > nextSpawn){
            nextSpawn = Time.time + spawnRate;
            //randX = Random.Range(-8.4f, 8.4f);
            GameObject chosen = null;
            
            float random = Random.Range(1, 100);
            int minR = 0;
            for (int j = 0; j < prob.Length; j++){
                int maxR = minR + prob[j];
                if (random > minR && random <= maxR){
                    chosen = enemy[j];
                    break;
                }
                minR = maxR;
            }
            
            whereToSpawn = new Vector2 (transform.position.x, transform.position.y);
            Instantiate(chosen, whereToSpawn, Quaternion.identity);
            //numberOfEnemies--;
            cnt++;
            total = numberOfEnemies;
        }   
    }
}
