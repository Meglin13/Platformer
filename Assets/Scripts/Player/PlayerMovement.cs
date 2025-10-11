using Entities.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField]
    private int MaxHealth = 100;
    public int health;
    [SerializeField]
    private HealthStat healthStat;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    public float limitVelocity = 100f;

    private float horizontalMovement;

    [Header("Jumping")]
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private int maxJumpsInAir = 2;
    private int jumpsRemaining;

    [Header("Ground Checking")]
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float rayStartOffset = 0.5f;
    [SerializeField] private LayerMask layer;

    private readonly Vector2[] rayDirections = new Vector2[]
    {
        Vector2.down,                   // вниз
        Vector2.left,                   // влево
        Vector2.right,                  // вправо
        new Vector2(-1, -1).normalized, // вниз-влево
        new Vector2(1, -1).normalized   // вниз-вправо
    };

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumpsInAir;

        healthStat = new HealthStat(MaxHealth);

    }

    private void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (rb.velocity.magnitude > limitVelocity)
            rb.velocity = rb.velocity.normalized * limitVelocity;

        if (CheckAnyDirection())
            jumpsRemaining = maxJumpsInAir;

        health = healthStat.CurrentValue;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpsRemaining--;
        }
    }

    private bool CheckAnyDirection()
    {
        Vector2 rayStart = (Vector2)transform.position + Vector2.up * rayStartOffset;

        foreach (Vector2 dir in rayDirections)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayStart, dir, raycastDistance, layer);
#if UNITY_EDITOR
            Debug.DrawRay(rayStart, dir * raycastDistance, hit.collider ? Color.red : Color.green);
#endif
            if (hit.collider != null)
                return true;
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        Gizmos.color = Color.green;

        Vector2 rayStart = (Vector2)transform.position + Vector2.up * rayStartOffset;

        foreach (Vector2 ray in rayDirections)
        {
            Gizmos.DrawLine(rayStart, rayStart + ray * raycastDistance);
        }
    }

#endif

    // TODO: Получение дамага
    public void TakeDamage(int damage)
    {
        healthStat.ChangeValue(-damage);
    }
}
