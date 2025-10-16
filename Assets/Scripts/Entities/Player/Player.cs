using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  ласс игрока, наследуемый от EntityScript. ”правление и особа€ логика игрока.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entities.EntityScript
{
    [Header("Stats")]
    [SerializeField] private MoneyStat money = new MoneyStat();

    [SerializeField] private int stompDamage = 10;
    public int StompDamage => stompDamage;

    public MoneyStat Money => money;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    public float limitVelocity = 100f;

    private float horizontalMovement;

    [Header("Jumping")]
    [SerializeField] private float jumpPower = 10f;

    [SerializeField] private int maxJumpsInAir = 2;
    private int jumpsRemaining;

    protected override void Awake()
    {
        base.Awake();

        jumpsRemaining = maxJumpsInAir;

        money = new MoneyStat();

        OnDie += () => SceneManager.Instance.ReloadCurrentScene();
    }

    private void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (rb.velocity.magnitude > limitVelocity)
            rb.velocity = rb.velocity.normalized * limitVelocity;

        if (CheckAnyDirection())
            jumpsRemaining = maxJumpsInAir;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;

        if (horizontalMovement != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontalMovement);
            transform.localScale = scale;
            OnMove?.Invoke();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpsRemaining > 0)
        {
            Bounce(jumpPower);
            TriggerJump();
            jumpsRemaining--;
        }
    }

    public void Bounce(float bounceMultiplier)
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceMultiplier);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Money.ClearEvents();
    }
}