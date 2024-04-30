using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IOpening
{
    [SerializeField] private int openSpeed = 50;
    [SerializeField] private int extraOpeningHeight = 0;
    private bool isOpen = false;
    private Vector3 originalPosition;
    private float colliderHeight;

    public void Open()
    {
        isOpen = true;
    }

    void Start()
    {
        originalPosition = transform.position;
        colliderHeight = GetComponent<BoxCollider2D>().bounds.size.y;
    }

    void Update()
    {
        if (isOpen && transform.position.y < originalPosition.y + colliderHeight + extraOpeningHeight)
        {
            transform.Translate(Vector3.right * openSpeed * Time.deltaTime);
        }
    }
}
