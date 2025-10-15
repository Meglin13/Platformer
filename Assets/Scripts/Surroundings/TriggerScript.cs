using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Скрипт для любых триггеров
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Triggercript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            unityEvent?.Invoke();
        }
    }
}