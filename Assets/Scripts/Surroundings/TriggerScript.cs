using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Скрипт для любых триггеров
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Triggercript : MonoBehaviour
{
    protected Player Player;

    public UnityEvent playerEvent;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Player = player;
            playerEvent?.Invoke();
        }
    }
}