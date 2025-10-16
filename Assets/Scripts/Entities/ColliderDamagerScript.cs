using Entities.Interfaces;
using UnityEngine;

/// <summary>
/// ����� ��������, ������� ����� �������� ���� ������
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
    /// �������� �������� ������, ����� ��� ������ ��� ������ �� �������� ����
    /// </summary>
    /// <param name="collision">��������</param>
    /// <returns>���� �� �������� ������</returns>
    private bool IsCollisionFromTop(Collision2D collision) => collision.contacts[0].normal.y < -0.5f;

    private void DamageEntity(IDamageable damageable) => damageable.TakeDamage(damageAmount);
}