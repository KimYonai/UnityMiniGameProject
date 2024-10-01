using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] PlayerController player;
    [SerializeField] float speed;

    private void Start()
    {
        //player = GetComponent<PlayerController>();
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 3.0f);
    }

    //private void OnEnable()
    //{
    //    rb.AddRelativeForce(rb.velocity);
    //}

    private void Update()
    {
        //transform.Translate(Vector2.one * speed * Time.deltaTime);
        

        //player.gameObject.render.flipX
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
