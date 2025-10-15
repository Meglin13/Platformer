using UnityEngine;

/// <summary>
/// Скрипт собираемых предметов. Можно добавить логику восстановления прыжков, ускорения и т.д.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(SpriteRenderer))]
public class CollectableScript : MonoBehaviour, ICollectable
{
    [Header("Components")]
    private AudioSource source;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Collect(player);
        }
    }

    public virtual void Collect(Player player)
    {
        source.Play();
        circleCollider.enabled = false;
        spriteRenderer.enabled = false;
    }
}