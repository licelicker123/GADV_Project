using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private bool isMoving;

    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite moveSprite;
    public Sprite jumpSprite;

    private float jumpForce = 22f;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    private void Start()
    {
        spriteRenderer.sprite = idleSprite;
    }
    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); //a/d or left/right arrow
        playerRb.velocity = new Vector2(input * moveSpeed, playerRb.velocity.y); // Move left/right
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


        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer); //create a circle at our feet to check if it overlaps with ground
        //jump character
        //if (isGrounded == true && Input.GetButton("Jump"))
        //{
          //  playerRb.velocity = Vector2.up * jumpForce;
        //}
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        if (!isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else if (isMoving)
        {
            spriteRenderer.sprite = moveSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

    }
}

