using UnityEngine;

public class SimpleResetScene : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}