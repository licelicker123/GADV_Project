using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DirectorAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    public float distanceBetween;
    private bool isFacingRight = true;

    //patrol mechanic
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float patrolSwitchDelay = 0.5f;
    private float patrolCooldown = 0f;
    public float switchThreshold = 0.2f;

    //player chase
    private bool isChasing = false;
    public PlayerHiding playerHiding;
    public float chaseRange;

    //sound trigger investigate
    private Vector3 soundPosition;
    private bool triggeredBySound = false;
    public float triggeredBySoundDuration = 4f;
    private float soundTimer = 0f;

    //speed control
    private float patrolSpeed = 10f;
    private float chaseSpeed = 20f;
    private float investigateSpeed = 15f;
    private float currentSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        currentSpeed = patrolSpeed;
        if (player != null)
        {
            playerHiding = player.GetComponent<PlayerHiding>();
            if (playerHiding == null)
            {
                Debug.LogError("PlayerHiding component is missing on the Player object!");
            }
        }
        else
        {
            Debug.LogError("Player reference is not assigned in DirectorAI!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        isChasing = distance < distanceBetween;

        Debug.Log("Current speed:" + currentSpeed);

        if (isChasing)
        {
            bool playerInRange = Vector2.Distance(transform.position, player.transform.position) < chaseRange;
            if (!playerHiding.isHidden && playerInRange)
            {
                triggeredBySound = false;
                currentSpeed = chaseSpeed;
                EnemyChase();
                Debug.Log("Director is chasing player");
            }
            else
            {
                currentSpeed = patrolSpeed;
                Patrol();
                Debug.Log("Director is patrolling the hallways...");
            }
        }
        else if (triggeredBySound)
        {
            currentSpeed = investigateSpeed;
            FollowsSound();
            Debug.Log("Director is investigating the sound");
        }
        else
        {
            currentSpeed = patrolSpeed;
            Patrol();
            Debug.Log("Director is patrolling the hallways...");
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
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
        playerRb.MovePosition(newPos);
        float direction = Mathf.Sign(currentPoint.position.x - transform.position.x);

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
        if (playerHiding != null && !playerHiding.isHidden)
        {
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
            playerRb.MovePosition(newPos);

            float direction = Mathf.Sign(player.transform.position.x - transform.position.x);

            // Flip if needed
            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            {
                Flip();
            }
        }

    }
    public void HearSound(Vector3 position)
    {
        soundPosition = position;
        triggeredBySound = true;
        soundTimer = triggeredBySoundDuration;
    }

    private void FollowsSound()
    {
        if (triggeredBySound)
        {
            soundTimer -= Time.deltaTime;

            // Move toward the sound source
            Vector2 newPosition = Vector2.MoveTowards(transform.position, soundPosition, currentSpeed * Time.deltaTime);
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