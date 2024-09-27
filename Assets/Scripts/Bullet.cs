using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 destination;
    [SerializeField] float speed;

    private void Start()
    {
        rb.velocity = Vector2.left * speed;
    }

    public void SetDestination(Vector2 destination)
    {
        this.destination = destination;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }

        Destroy(gameObject);
    }
}
