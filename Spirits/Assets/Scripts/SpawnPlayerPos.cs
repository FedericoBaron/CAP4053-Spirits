using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerPos : MonoBehaviour
{
    // Start is called before the first frame update
    public int x = 0;
    public int y = 0;
    public int z = 0;
    public GameObject currPlayer;
    
    void Awake()
    {
        GameObject player = GameObject.Find("Bartender");
        Vector3 pos = transform.position;
        Debug.Log(player);
        if (player == null){
            Vector2 whereToSpawn = new Vector2(pos.x, pos.y);
            player = Instantiate(currPlayer, whereToSpawn, Quaternion.identity);
            player.name = "Bartender";
            Debug.Log(player);
        }
        player.transform.position = new Vector3(pos.x, pos.y, 0);   
        Debug.Log(player.transform.position.x + " " + player.transform.position.y + " " + player.transform.position.z);
    }
}
