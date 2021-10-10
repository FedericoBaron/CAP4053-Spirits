using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Motion : MonoBehaviour
{
    Rigidbody2D playerBody;
    Rigidbody2D bottle;

    private float gravity = 9.81f;
    private float time = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        bottle = GetComponent<Rigidbody2D>();

        transform.parent = null;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
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
}
