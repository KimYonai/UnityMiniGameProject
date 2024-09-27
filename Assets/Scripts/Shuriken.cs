using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;

    private void Start()
    {
        rb.velocity = Vector2.right * speed;
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Ãæµ¹");
        if (collider.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = collider.gameObject.GetComponent<EnemyController>();
            enemyController.TakeHit();
        }

        Destroy(gameObject);
    }
}
