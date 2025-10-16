using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������ ��� �������� ��������� �� �������
/// </summary>
public class SimplePlayerFollow : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
}