using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Aiming : MonoBehaviour
{
    // We'll need Lines 7-8 to manipulate the position/rotation/scale and animation triggers
    Rigidbody2D playerBody;
    Animator playerAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Line 20 gets the cursor position relative to the world origin, not the camera bounds
        // (This helps later with the vector math)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Vector2 playerPos = new Vector2(playerBody.position.x, playerBody.position.y);

        // Creates a vector based on the position of the player and cursor, then turns it into an angle
        Vector2 distance = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y);
        float angle = (Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg);
        if(mousePos.x < playerPos.x)
            angle += 90;
        else
            angle -= 90;
        // Lines 28-31 convert the arctangents into 0-180 on the left and 0-(-180) on the right

        float absAngle = Mathf.Abs(angle);
        float absDistance = Mathf.Abs(distance.x);
        float dirScale = distance.x / absDistance;

        // Based on the angle/position of the cursor, we'll alter the sprite's direction to make it seem like the player is facing that way
        if(absAngle <= 45)
        {
            // General Direction is UP
            playerAnim.SetInteger("Direction", 0);

            if(Input.GetAxis("Vertical") < 0)
                playerAnim.SetBool("Reverse", true);
            else if(Input.GetAxis("Vertical") > 0)
                playerAnim.SetBool("Reverse", false);
        }
        else if(absAngle > 45 && absAngle <= 135)
        {
            // General Direction is LEFT or RIGHT
            transform.localScale = new Vector2(dirScale, 1);
            playerAnim.SetInteger("Direction", 1);

            if((Input.GetAxis("Horizontal") < 0 && dirScale == 1) || (Input.GetAxis("Horizontal") > 0 && dirScale == -1))
                playerAnim.SetBool("Reverse", true);
            else if((Input.GetAxis("Horizontal") > 0 && dirScale == 1) || (Input.GetAxis("Horizontal") < 0 && dirScale == -1))
                playerAnim.SetBool("Reverse", false);
        }
        else
        {
            // General Direction is DOWN
            playerAnim.SetInteger("Direction", 2);

            if(Input.GetAxis("Vertical") > 0)
                playerAnim.SetBool("Reverse", true);
            else if(Input.GetAxis("Vertical") < 0)
                playerAnim.SetBool("Reverse", false);
        }
        
    }
}
