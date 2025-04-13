using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class player2 : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 40f;
    public float rayLength = 0.5f;
    public LayerMask groundLayer;

    // Referencia a la cámara
    public Camera mainCamera;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    public GameManager gameManager;
    private bool isDead = false;

    // Posición original de la cámara
    private Vector3 originalCameraPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Si no se asigna la cámara, usar la principal
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Guardar la posición original de la cámara
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
        }
    }

    void Update()
    {
        if (isDead || gameManager.gameState != GameState.Playing)
            return;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDead)
        {
            print("El jugador murió. RIP.");
            isDead = true;
            anim.SetTrigger("isDead");

            // Desactivar controles del jugador
            this.enabled = false;

            // Hacer que el jugador traspase plataformas
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                playerCollider.isTrigger = true;
            }

            // Aplicar fuerza hacia abajo
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.5f, -2f);

            // Congelar la cámara en su posición actual
            StartCoroutine(FreezeCameraAndRestart(4f));
        }
    }

    IEnumerator FreezeCameraAndRestart(float delay)
    {
        // Congelar la cámara creando un objeto en su posición actual
        if (mainCamera != null)
        {
            // Crear un objeto vacío en la posición actual de la cámara
            GameObject cameraHolder = new GameObject("CameraHolder");
            cameraHolder.transform.position = mainCamera.transform.position;

            // Hacer que la cámara sea hija de este objeto
            mainCamera.transform.parent = cameraHolder.transform;
        }

        // Esperar el tiempo de la animación de muerte
        yield return new WaitForSeconds(delay);

        // Reiniciar nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}