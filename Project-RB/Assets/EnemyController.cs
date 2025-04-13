using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);

            movement = new Vector2(direction.x, 0);
            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        animator.SetBool("enMovimiento", enMovimiento);
    }

  /*  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }*/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
