using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponent<Player>();
    }
}