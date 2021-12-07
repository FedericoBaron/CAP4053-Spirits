using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Animator playerAnim;

    // Base speed vector that we'll use to multiply by our movement booleans
    public Vector2 speed;
    public bool isDashing;
    float dashTime;
    readonly int dashSpeed = 4;
    readonly float dashLen = 0.2f;
    float nextDashTime = 0f;

    void Start()
    {
        speed = new Vector2(8, 6);
        isDashing = false;

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

        if(Input.GetKeyDown(KeyCode.X) && Time.time >= nextDashTime){
            GetComponents<AudioSource>()[3].Play();
			speed.x = speed.x * dashSpeed;
            speed.y = speed.y * dashSpeed;
            isDashing = true;
            dashTime = Time.time + dashLen;
            nextDashTime = Time.time + 1.5f;
		}

        if(isDashing && dashTime < Time.time){
            speed.x = speed.x/dashSpeed;
            speed.y = speed.y/dashSpeed;
            isDashing = false;
        }

        Vector2 movement = new Vector2(speed.x * InputX, speed.y * InputY);
        movement *= Time.deltaTime;
        // Line 18 prevents the sprite from accelerating violently off the screen

        transform.Translate(movement);
    }
}
