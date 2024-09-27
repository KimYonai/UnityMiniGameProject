using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer enemyRender;
    [SerializeField] Transform target;
    [SerializeField] Vector2 destination;
    [SerializeField] float speed;

    private void Start()
    {
        rb.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.TakeHit();
        }

        Destroy(gameObject);
    }
}
