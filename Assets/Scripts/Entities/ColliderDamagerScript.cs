using Entities.Interfaces;
using UnityEngine;

/// <summary>
/// Класс объектов, которые могут наносить урон игроку
/// </summary>
public class ColliderDamagerScript : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;


    [SerializeField] private float bounceForce = 5f;

    [SerializeField] private bool damageFromTop = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            bool isTopCollision = IsCollisionFromTop(collision);

            if ((damageFromTop) || (!damageFromTop && !isTopCollision))
            {
                DamageEntity(player);
                player.Bounce(bounceForce);
            }
        }
    }

    /// <summary>
    /// Проверка коллизии сверху, чтобы при победы над врагом не получать урон
    /// </summary>
    /// <param name="collision">Коллизия</param>
    /// <returns>Есть ли коллизия сверху</returns>
    private bool IsCollisionFromTop(Collision2D collision) => collision.contacts[0].normal.y < -0.5f;

    private void DamageEntity(IDamageable damageable) => damageable.TakeDamage(damageAmount);
}