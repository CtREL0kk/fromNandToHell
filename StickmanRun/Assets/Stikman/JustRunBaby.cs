using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{

    [SerializeField] private float _RotateSpeed;
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private BoxCollider2D _checkGround;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator an;
    BoxCollider2D cl;
    public bool _isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        HorizontalMove();
        VerticalMove();
    }

    private void HorizontalMove()
    {
       // var direction = Input.GetAxis("Horizontal");
       // sr.flipX = direction < 0 ? true : false;

        var value = _MoveSpeed * Time.deltaTime;
        transform.Translate(value * Vector3.right);

        //an.SetFloat("Horizontal Move", Mathf.Abs(direction));
    }

    private void VerticalMove()
    {
        var direction = Input.GetAxis("Vertical");
        if (direction > 0 && _isGrounded)
        {
            rb.AddForce(Vector3.up * _forceJump, ForceMode2D.Impulse);
           // an.SetBool("Jumping", true);
            _isGrounded = false;
        }
       // else
         //   an.SetBool("Jumping", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isGrounded = true;
    }
    /*
    private bool IsGrounded()
    {
        var listOfColliders = Physics2D.OverlapBoxAll(_checkGround.transform.position, _checkGround.size, 0);

        return listOfColliders.Length > 2;
    }
    */
}
