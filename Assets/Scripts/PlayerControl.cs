using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    public SpriteRenderer spriteRenderer;
    private float jumpForce = 22f;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); //a/d or left/right arrow
        playerRb.velocity = new Vector2(input * moveSpeed, playerRb.velocity.y); // Move left/right

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
    }
}

