using UnityEngine;

public class DirectorAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D enemyRb;
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
    public float chaseRange = 50f; //how far must player be to start chase
    private bool pauseAfterLosingPlayer = false; //if player not seen by director midchase
    public float pauseTime = 2f; //how long to stay near player location
    private float pauseTimer = 0f;

    //sound trigger investigate
    private Vector3 soundPosition;
    private bool triggeredBySound = false;
    public float triggeredBySoundDuration = 2.5f; 
    private float soundTimer = 0f; 
    private bool isInvestigating = false;
    public float investigationTime = 2f; // how long to stay at sound location
    private float investigationTimer = 0f;


    //speed control
    private float patrolSpeed = 40f;
    private float chaseSpeed = 70f;
    private float investigateSpeed = 60f;
    private float currentSpeed;

    //detection ui
    public GameObject eyeClosed;
    public GameObject eyeInvestigating;
    public GameObject eyeChasing;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform; 
        currentSpeed = patrolSpeed;
        eyeClosed.SetActive(true);
        eyeInvestigating.SetActive(false);
        eyeChasing.SetActive(false);

        //debug checks
        if (player != null)
        {
            playerHiding = player.GetComponent<PlayerHiding>();
        }
        else
        {
            Debug.LogError("Player reference is not assigned in DirectorAI");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // checking if player in range first
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        bool playerInRange = distanceToPlayer < chaseRange;
        bool playerVisible = !playerHiding.isHidden;

        if (playerInRange && playerVisible)
        {
            //chase player
            triggeredBySound = false;
            isInvestigating = false;
            pauseAfterLosingPlayer = false;

            eyeClosed.SetActive(false);
            eyeInvestigating.SetActive(false);
            eyeChasing.SetActive(true);

            currentSpeed = chaseSpeed;
            EnemyChase();
            Debug.Log("Director is chasing player!");
            return;
        }
        else if (playerInRange && !playerVisible && isChasing)
        {
            //if player hide mid chase
            isChasing = false;

            eyeClosed.SetActive(true);
            eyeInvestigating.SetActive(false);
            eyeChasing.SetActive(false);

            pauseAfterLosingPlayer = true;
            pauseTimer = pauseTime;
            enemyRb.velocity = Vector2.zero; //stop moving
            Debug.Log("Director lost sight of player, waiting...");
        }
        //enemy pausing aft losing the player
        if (pauseAfterLosingPlayer)
        {
            pauseTimer -= Time.deltaTime; //countdown timer till can move again
            enemyRb.velocity = Vector2.zero;//stop moving

            if (pauseTimer <= 0f)
            {
                pauseAfterLosingPlayer = false;
                //randomly choose direction to patrol 
                currentPoint = (Random.value < 0.5f) ? pointA.transform : pointB.transform;
            }
            return;
        }


        // follow sound
        if (triggeredBySound)
        {
            currentSpeed = investigateSpeed;

            eyeClosed.SetActive(false);
            eyeInvestigating.SetActive(true);
            eyeChasing.SetActive(false);

            FollowsSound();
            Debug.Log("Director is investigating sound.");
            return;

        }

        //patrol
        currentSpeed = patrolSpeed;

        eyeClosed.SetActive(true);
        eyeInvestigating.SetActive(false);
        eyeChasing.SetActive(false);

        Patrol();
        Debug.Log("Director is patrolling the hallways...");
    }



    private void Patrol()
    {
        //for director going back and forth

        if (patrolCooldown > 0)
        {
            patrolCooldown -= Time.deltaTime;
            enemyRb.velocity = Vector2.zero;
            return; // wait before switching patrol direction
        }

        Vector2 targetPos = new Vector2(currentPoint.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
        enemyRb.MovePosition(newPos);
        float direction = Mathf.Sign(currentPoint.position.x - transform.position.x);

        //flip !
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }

        // switch to patrol to other point if its close
        if (Vector2.Distance(transform.position, currentPoint.position) < switchThreshold)
        {
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
            patrolCooldown = patrolSwitchDelay;
        }
    }

    //chase function
    private void EnemyChase()
    {
        if (playerHiding != null && !playerHiding.isHidden)
        {
            //move in players direction
            Vector2 targetPos = new Vector2(player.transform.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
            enemyRb.MovePosition(newPos);

            float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
            isChasing = true;

            // flip!
            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            {
                Flip();
            }
        }

    }

    //can hear sound
    public void HearSound(Vector3 position)
    {
        soundPosition = position;
        triggeredBySound = true;
        soundTimer = triggeredBySoundDuration;
    }

    //follow sound function aft sound is triggered
    private void FollowsSound()
    {
        soundTimer -= Time.deltaTime;

        float distanceToSound = Vector2.Distance(transform.position, soundPosition);

        if (distanceToSound > 0.2f)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, soundPosition, currentSpeed * Time.deltaTime);
            enemyRb.MovePosition(newPosition);

            // flip!
            if ((soundPosition.x > transform.position.x && !isFacingRight) ||
                (soundPosition.x < transform.position.x && isFacingRight))
            {
                Flip();
            }
        }
        else
        {
            // investigate sound... pause for a while
            isInvestigating = true;
            investigationTimer = investigationTime;
        }

        // done investigating, continue patrolling
        if (soundTimer <= 0f && !isInvestigating)
        {
            triggeredBySound = false;
            currentPoint = (Random.value < 0.5f) ? pointA.transform : pointB.transform;
        }
    }


    // flip function
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}