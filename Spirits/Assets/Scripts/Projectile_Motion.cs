using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Motion : MonoBehaviour
{
    Rigidbody2D playerBody;
    Rigidbody2D bottle;
    Player_Combat player;
    public GameObject[] attackEffect;
    private float gravity = 9.81f;
    private float time = 1.5f;
    public Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        bottle = GetComponent<Rigidbody2D>();

        transform.parent = null;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Vector2 playerPos = new Vector2(playerBody.position.x, playerBody.position.y);

        // Creates a vector based on the position of the player and cursor, then turns it into an angle
        Vector2 distance = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y);

        bottle.velocity = new Vector2(distance.x / time, (mousePos.y + 0.5f * gravity * time * time - playerPos.y) / time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 180 * Time.deltaTime);
        Destroy(gameObject, time);
    }

    void makeEffect(){
        player = GameObject.Find("Bartender").GetComponent<Player_Combat>();
        Debug.Log("x" + mousePos.ToString());

        if (player.attackType != -1){
            GameObject bottle = Instantiate(attackEffect[player.attackType], new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            bottle.transform.parent = player.gameObject.transform;
        }
    }

    void OnDestroy(){
        //Debug.Log("print destroyed this object");
        if(!this.gameObject.scene.isLoaded) return;
        makeEffect();
    }

}   