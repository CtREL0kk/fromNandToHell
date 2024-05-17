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
    private bool _isGrounded => 
        Physics2D.OverlapArea(_checkGround.bounds.min, _checkGround.bounds.max, LayerMask.GetMask("Ground"));

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
        if (Input.GetKeyDown(KeyCode.S) && _isGrounded) an.SetTrigger("Tackle");
        else an.ResetTrigger("Tackle");
    }

    private void HorizontalMove()
    {

        var value = _MoveSpeed * Time.deltaTime;
        transform.Translate(value * Vector3.right);
    }

    private void VerticalMove()
    {
        var direction = Input.GetAxis("Vertical");
        if (direction > 0 && _isGrounded && !an.GetCurrentAnimatorStateInfo(0).IsName("Tackle"))
        {
            rb.AddForce(Vector3.up * _forceJump, ForceMode2D.Impulse);
            an.SetTrigger("Jump");
        }
        else
            an.ResetTrigger("Jump");
    }
    
}
