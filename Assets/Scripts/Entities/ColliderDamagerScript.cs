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

    private bool IsCollisionFromTop(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        return contact.normal.y < -0.5f;
    }

    private void DamageEntity(IDamageable damageable)
    {
        damageable.TakeDamage(damageAmount);
    }
}