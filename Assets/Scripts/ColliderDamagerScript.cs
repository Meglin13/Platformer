using Entities.Interfaces;
using System.Collections;
using System.Collections.Generic;
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
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
