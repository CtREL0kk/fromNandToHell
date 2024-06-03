using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos, length, startPosY;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        
        float movement = cam.transform.position.x * (1 - parallaxEffect);
        
        transform.position = new Vector3(startPos + distance, transform.position.z);
        if (movement > startPos + length)
            startPos += length;
        else if (movement < startPos - length)
            startPos -= length;
    }
}
