using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;

    private void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        direction = playerPos.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * 100000);

        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = GetComponent<PlayerController>();
            player.TakeHit();
            Destroy(gameObject);
        }
        else if (collision != null)
        {
            Destroy(gameObject);
        }
    }
}
