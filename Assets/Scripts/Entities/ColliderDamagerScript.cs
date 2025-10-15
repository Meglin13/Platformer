using Entities.Interfaces;
using UnityEngine;

/// <summary>
/// Класс объектов, которые могут наносить урон игроку
/// </summary>
public class ColliderDamagerScript : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable) && collision.gameObject.layer != gameObject.layer)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}