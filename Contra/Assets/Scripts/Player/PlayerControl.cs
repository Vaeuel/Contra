using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        //float vInput = Input.GetAxis("Verticle");

        rb.velocity = new Vector2 (hInput * speed, rb.velocity.y);

    }
}
