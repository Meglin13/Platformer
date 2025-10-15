using UnityEngine;

namespace Entities
{
    public class EntityAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private EntityScript entityScript;
        [SerializeField] private Rigidbody2D rb;

        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        private static readonly int Die = Animator.StringToHash("Die");

        private float movementThreshold = 0.1f;
        private float fallThreshold = -0.1f;
        private float jumpThreshold = 0.1f;

        private void Awake()
        {
            if (!animator) animator = GetComponent<Animator>();
            if (!entityScript) entityScript = GetComponent<EntityScript>();
            if (!rb) rb = GetComponent<Rigidbody2D>();

            if (entityScript)
            {
                entityScript.OnTakeDamage += HandleTakeDamage;
                entityScript.OnDie += HandleDie;
            }
        }

        private void Update()
        {
            UpdateAnimationStates();
        }

        private void UpdateAnimationStates()
        {
            if (!animator || !rb) return;

            float horizontalVelocity = Mathf.Abs(rb.velocity.x);
            float verticalVelocity = rb.velocity.y;

            bool isWalking = horizontalVelocity > movementThreshold;
            bool isFalling = verticalVelocity < fallThreshold;
            bool isJumping = verticalVelocity > jumpThreshold;

            animator.SetBool(IsWalking, isWalking);
            animator.SetBool(IsFalling, isFalling);
            animator.SetBool(IsJumping, isJumping);
        }

        private void HandleTakeDamage()
        {
            if (animator) animator.SetTrigger(TakeDamage);
        }

        private void HandleDie()
        {
            if (animator) animator.SetTrigger(Die);
        }

        private void OnDestroy()
        {
            if (entityScript)
            {
                entityScript.OnTakeDamage -= HandleTakeDamage;
                entityScript.OnDie -= HandleDie;
            }
        }
    }
}