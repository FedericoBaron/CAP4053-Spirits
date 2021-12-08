using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerPos : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = 0;
    public float y = 0;
    public float z = 0;
    public GameObject currPlayer;
    
    void Awake()
    {
        GameObject player = GameObject.Find("Bartender");
        Debug.Log(player);
        if (player == null){
            Vector2 whereToSpawn = new Vector2(x, y);
            player = Instantiate(currPlayer, whereToSpawn, Quaternion.identity);
            player.name = "Bartender";
            Debug.Log(player);
        }
        player.transform.position = new Vector3(x, y, z);   
        Debug.Log(player.transform.position.x + " " + player.transform.position.y + " " + player.transform.position.z);
    }
}