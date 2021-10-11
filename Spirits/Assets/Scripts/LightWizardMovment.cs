using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWizardMovment : MonoBehaviour
{
    [SerializeField] private float SPEED = 5;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * SPEED, rb.velocity.y);
        
        if (Input.GetKey(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, SPEED);
        }
        if (horizontal < 0)
        {
            this.transform.rotation = new Quaternion(0, -1, 0, 0);
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 1, 0, 0);
        }
    }
}
