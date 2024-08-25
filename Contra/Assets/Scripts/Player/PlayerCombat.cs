using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerCombat : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask WaterLayer;

   // private bool isFiring = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("IsFiring", isFiring);
        }*/

    }
}
