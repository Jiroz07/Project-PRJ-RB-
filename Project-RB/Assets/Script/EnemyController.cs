using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 1.5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private bool isInDetectionRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius && !isInDetectionRange)
        {
            isInDetectionRange = true;
        }
        else if (distanceToPlayer >= detectionRadius && isInDetectionRange)
        {
            isInDetectionRange = false;
        }

        if (isInDetectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);
            movement = new Vector2(direction.x, 0);
            animator.SetBool("enMovimiento", true);
        }
        else
        {
            movement = Vector2.zero;
            animator.SetBool("enMovimiento", false);
        }

        if (movement != Vector2.zero)
        {
            rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
