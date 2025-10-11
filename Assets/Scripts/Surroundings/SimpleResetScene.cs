using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleResetScene : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private void Update()
    {
        transform.position =  new Vector2(player.transform.position.x, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}