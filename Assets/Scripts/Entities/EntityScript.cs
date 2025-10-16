using Entities.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    /// <summary>
    /// Скрипт для любых сущностей, которые могут взаимодейстовать друг с другом. В этом классе собрана общая логика: урон, детекция земли пр.
    /// </summary>
    public class EntityScript : MonoBehaviour, IDamageable
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected SpriteRenderer spriteRenderer;

        [SerializeField] private HealthStat healthStat = new HealthStat();
        public HealthStat Health => healthStat;

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

        // События
        public Action OnFall = delegate { };
        public Action OnTakeDamage = delegate { };
        public Action OnJump = delegate { };
        public Action OnMove = delegate { };
        public Action OnDie = delegate { };
        public Action OnLand = delegate { };

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            OnDie += () => Die();
        }

        protected virtual void Update()
        {
            CheckFallState();
            CheckMovementState();
        }

        /// <summary>
        /// Очистка событий при унитожении
        /// </summary>
        protected virtual void OnDestroy()
        {
            Health.ClearEvents();
            OnTakeDamage = null;
            OnJump = null;
            OnMove = null;
            OnDie = null;
            OnLand = null;
        }

        private bool wasFalling = false;

        private void CheckFallState()
        {
            bool isFalling = rb.velocity.y < -0.1f && !CheckAnyDirection();

            if (isFalling && !wasFalling)
            {
                OnFall?.Invoke();
            }
            else if (!isFalling && wasFalling)
            {
                OnLand?.Invoke();
            }

            wasFalling = isFalling;
        }

        private void CheckMovementState()
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
            {
                OnMove?.Invoke();
            }
        }

        public void TriggerJump()
        {
            OnJump?.Invoke();
        }

        /// <summary>
        /// Метод обнаружения земли
        /// </summary>
        /// <returns>Есть ли рядом земля</returns>
        public bool CheckAnyDirection()
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

        private Coroutine damageFlashCoroutine;

        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="damage">Получаемый урон. Если поменять знак, то получаемое лечение.</param>
        public void TakeDamage(int damage)
        {
            healthStat.ChangeValue(-damage);

            if (damageFlashCoroutine != null)
            {
                StopCoroutine(damageFlashCoroutine);
            }
            damageFlashCoroutine = StartCoroutine(DamageFlashCoroutine());

            OnTakeDamage?.Invoke();

            if (healthStat.CurrentValue <= 0)
            {
                OnDie?.Invoke();
            }
        }

        protected void Die()
        {
            gameObject.SetActive(false);
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

        private Color originalColor = Color.clear;

        /// <summary>
        /// Корутин для отображения получения урона.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DamageFlashCoroutine()
        {
            if (spriteRenderer == null) yield break;

            if (originalColor == Color.clear)
            {
                originalColor = spriteRenderer.color;
            }

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }


    }
}