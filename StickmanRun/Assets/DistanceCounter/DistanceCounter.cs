using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject player;
    private Text distanceText;
    public float distance { get; private set; }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        player.transform.position = startPosition;
    }

    void Update()
    {
        distance = (int)(player.transform.position.x - startPosition.x);
        distanceText.text = $"{distance} m";
    }
}
