using UnityEngine;

public class player2 : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; 
    public float rayLength = 0.5f; 
    public LayerMask groundLayer; 

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); 
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y); 

        isGrounded = IsGrounded(); 

        
        if (anim != null)
        {
            anim.SetBool("isRunning", moveInput != 0); 
            anim.SetBool("isJumping", !isGrounded); 
        }

        
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump(); 
        }

        
        if (moveInput > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (moveInput < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
    }


    
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
    }

    
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        UnityEngine.Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        return hit.collider != null; 
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
}
