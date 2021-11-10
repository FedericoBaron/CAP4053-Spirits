using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Animator playerAnim;

    // Base speed vector that we'll use to multiply by our movement booleans
    public Vector2 speed;

    void Start()
    {
        speed = new Vector2(8, 6);
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // "Horizontal" corresponds to A, D, Left, Right; "Vertical" corresponds to W, S, Up Down
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");

        if((InputX != 0.0f) || (InputY != 0.0f))
            playerAnim.SetBool("Moving", true);
        else
            playerAnim.SetBool("Moving", false);

        Vector2 movement = new Vector2(speed.x * InputX, speed.y * InputY);
        movement *= Time.deltaTime;
        // Line 18 prevents the sprite from accelerating violently off the screen

        transform.Translate(movement);
    }
}
