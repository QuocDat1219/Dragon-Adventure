using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coc : MonoBehaviour
{
    [SerializeField] private float left;
    [SerializeField] private float right;

    [SerializeField] private float jumpLength = 2f;  // chieu dai buoc nhay
    [SerializeField] private float jumpHeight = 2f; // nhay cao
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingleft = true;


    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // trang thai nhay fall
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
        // trang thai nhay idle
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }

    private void Move()
    {
        if (facingleft)
        {
            if (transform.position.x > left)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                 
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingleft = false;
            }
        }
        else
        {
            if (transform.position.x < right)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingleft = true;
            }
        }
    }
}
    
        
