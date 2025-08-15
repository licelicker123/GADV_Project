using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //move
    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private bool isMoving;

    //player sprites control
    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite moveSprite;
    public Sprite jumpSprite;

    //jump
    private float jumpForce = 22f;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    private void Start()
    {
        //default idle sprite
        spriteRenderer.sprite = idleSprite;
    }
    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); //a/d or left/right arrow
        playerRb.velocity = new Vector2(input * moveSpeed, playerRb.velocity.y); // move left/right
        isMoving = Mathf.Abs(input) > 0;
        //flip character
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        //jump
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer); //create a circle at our feet to check if it overlaps with ground

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        //sprite control
        if (!isGrounded) //jump
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else if (isMoving) //move
        {
            spriteRenderer.sprite = moveSprite;
        }
        else //default
        {
            spriteRenderer.sprite = idleSprite;
        }

    }
}

