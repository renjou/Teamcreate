using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Player Movement
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), rb.linearVelocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
        {// ‰E•ûŒü‚ÌˆÚ“®“ü—Í
            Vector2 pos = transform.position;
            pos.x += 0.05f;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// ¶•ûŒü‚ÌˆÚ“®“ü—Í
            Vector2 pos = transform.position;
            pos.x -= 0.05f;
            transform.position = pos;
        }
    }
}