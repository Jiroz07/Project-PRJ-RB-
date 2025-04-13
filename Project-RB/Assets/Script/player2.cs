using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class player2 : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 40f;
    public float rayLength = 0.5f;
    public LayerMask groundLayer;
    public Camera mainCamera;
    public float deathUpForce = 10f;
    public float deathHorizontalForce = 2f;
    public float deathFallSpeed = 15f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isDead = false;
    private bool hasHitGroundAfterDeath = false;
    private float deathTime = 0f;
    private float deathFloorY = -100f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (Time.time - deathTime > 0.5f && rb.linearVelocity.y < 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.98f, -deathFallSpeed);

                if (!hasHitGroundAfterDeath)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
                    if (hit.collider != null || transform.position.y < deathFloorY)
                    {
                        hasHitGroundAfterDeath = true;
                        GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            return;
        }

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
        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDead)
        {
            isDead = true;
            deathTime = Time.time;

            RaycastHit2D floorHit = Physics2D.Raycast(transform.position, Vector2.down, 100f, groundLayer);
            if (floorHit.collider != null)
            {
                deathFloorY = floorHit.point.y;
            }

            if (anim != null)
            {
                anim.SetTrigger("isDead");
            }

            this.enabled = false;

            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                playerCollider.isTrigger = true;
            }

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            float directionX = transform.localScale.x < 0 ? deathHorizontalForce : -deathHorizontalForce;
            rb.linearVelocity = new Vector2(directionX, deathUpForce);

            FreezeCameraPosition();

            StartCoroutine(RestartLevelAfterDelay(3f));
        }
    }

    void FreezeCameraPosition()
    {
        if (mainCamera != null)
        {
            GameObject cameraHolder = new GameObject("CameraHolder");
            cameraHolder.transform.position = mainCamera.transform.position;

            mainCamera.transform.parent = cameraHolder.transform;

            Component[] cameraComponents = mainCamera.GetComponents<Component>();
            foreach (Component component in cameraComponents)
            {
                if (component is MonoBehaviour && !(component is Camera))
                {
                    ((MonoBehaviour)component).enabled = false;
                }
            }
        }
    }

    IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
