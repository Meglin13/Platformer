using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Скрипт для объектов следующих за игроком
/// </summary>
public class SimplePlayerFollow : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
}