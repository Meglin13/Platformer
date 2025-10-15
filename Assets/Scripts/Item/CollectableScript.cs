using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class CollectableScript : MonoBehaviour
{
    [SerializeField]
    private int amount = 1;

    [SerializeField]
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Money.ChangeValue(amount);
            source.Play();
            gameObject.SetActive(false);
        }
    }
}
