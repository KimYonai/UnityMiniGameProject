using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PooledObject pooledObject;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float returnTime;

    private float remainTime;

    private void OnEnable()
    {
        remainTime = returnTime;
    }

    private void Update()
    {
        rb.velocity = new Vector2(player.transform.position.x, player.transform.position.y) * speed;

        remainTime -= Time.deltaTime;

        if (remainTime < 0)
        {
            pooledObject.ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }
    }
}
