using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float maxDistance = 8f;

    private Rigidbody2D rb;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPosition = transform.position;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Destroy pizza when it reaches maximum distance.
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        if (rb == null)
            return;

        direction.Normalize();

        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Pizza hit: " + collision.gameObject.name);

        Destroy(gameObject);
    }
}
