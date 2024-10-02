using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer render;

    private void Start()
    {
        if (render.flipX == false)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
        

        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = collider.gameObject.GetComponent<EnemyController>();
            enemyController.TakeHit();
        }
        else if (collider.gameObject.tag == "Boss")
        {
            BossController bossController = collider.gameObject.GetComponent<BossController>();
            bossController.TakeHit();
        }

        Destroy(gameObject);
    }
}
