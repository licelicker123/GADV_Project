using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DirectorAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distance;
    public float distanceBetween;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            //move code
        }

    }
}
