using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 speed = new Vector2(8, 6);

    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(speed.x * InputX, speed.y * InputY);
        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
}
