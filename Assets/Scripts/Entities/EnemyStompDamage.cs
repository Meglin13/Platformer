using Entities.Interfaces;
using UnityEngine;

public class EnemyStompDamage : MonoBehaviour
{
    [SerializeField] private float bouncePower = 2;

    [SerializeField] private IDamageable damageable;

    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            CheckStompDamage(collision, player);
        }
    }

    private void CheckStompDamage(Collision2D collision, Player player)
    {
        ContactPoint2D contact = collision.contacts[0];

        if (contact.normal.y < -0.5f && player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            damageable.TakeDamage(player.StompDamage);
            player.Bounce(bouncePower);
        }
    }
}