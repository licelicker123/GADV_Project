using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DirectorAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 15f;
    public float distanceBetween;
    private bool isFacingRight = true;

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D playerRb;
    private Transform currentPoint;

    public float patrolSwitchDelay = 0.5f;
    private float patrolCooldown = 0f;
    public float switchThreshold = 0.2f;
    private bool isChasing = false;

    private Vector3 soundPosition;
    private bool triggeredBySound = false;
    public float triggeredBySoundDuration = 4f;
    private float soundTimer = 0f;

 
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;


    }

    // Update is called once per frame
    void Update()
    {


        float distance = Vector2.Distance(transform.position, player.transform.position);
        isChasing = distance < distanceBetween;

        if (isChasing)
        {
            triggeredBySound = false;
            EnemyChase();
            Debug.Log("Director is chasing player");
        }
        else if (triggeredBySound)
        {
            FollowsSound();
            Debug.Log("Director is investigating the sound");
        }
        else
        {
            Patrol();
        }
    }
    private void Patrol()
    {
        //for director going back and forth

        if (patrolCooldown > 0)
        {
            patrolCooldown -= Time.deltaTime;
            playerRb.velocity = Vector2.zero;
            return; // wait before switching patrol direction
        }

        Vector2 targetPos = new Vector2(currentPoint.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        playerRb.MovePosition(newPos);
        //Vector2 point = currentPoint.position - transform.position;
        float direction = Mathf.Sign(currentPoint.position.x - transform.position.x);
        //playerRb.velocity = new Vector2(direction * speed, playerRb.velocity.y);

        // Flip if needed
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }

        // Switch to the other point if close enough
        if (Vector2.Distance(transform.position, currentPoint.position) < switchThreshold)
        {
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
            patrolCooldown = patrolSwitchDelay;
        }
    }

    private void EnemyChase()
    {
        Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        playerRb.MovePosition(newPos);

        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);

        // Flip if needed
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }
    }
    public void HearSound(Vector3 position)
    {
        soundPosition = position;
        triggeredBySound= true;
        soundTimer = triggeredBySoundDuration;
    }

    private void FollowsSound()
    {
        if (triggeredBySound)
        {
            soundTimer -= Time.deltaTime;

            // Move toward the sound source
            Vector2 newPosition = Vector2.MoveTowards(transform.position, soundPosition, speed * Time.deltaTime);
            playerRb.MovePosition(newPosition);

            // Flip if necessary
            if ((soundPosition.x > transform.position.x && !isFacingRight) ||
                (soundPosition.x < transform.position.x && isFacingRight))
            {
                Flip();
            }

            if (soundTimer <= 0f)
            {
                triggeredBySound = false;
            }

            return; // Don't do anything else while chasing sound
        }

    }

    private void Flip()
    {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
    }

}
