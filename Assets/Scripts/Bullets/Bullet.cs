using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] PooledObject pooledObject;
    //[SerializeField] GameObject player;
    [SerializeField] Transform target;
    [SerializeField] Vector2 destination;
    //[SerializeField] float speed;
    //[SerializeField] float returnTime;
    //
    //private float remainTime;
    //
    //private void Start()
    //{
    //    //remainTime = returnTime;
    //    //player = GameObject.FindGameObjectWithTag("Player");
    //    //target = player.transform;
    //
    //    //transform.LookAt(destination);
    //    rb.velocity = player.transform.position;
    //    //rb.velocity = destination;
    //}
    //
    //public void SetDestination(Vector2 destination)
    //{
    //    this.destination = destination;
    //}
    //
    //private void Update()
    //{
    //    //remainTime -= Time.deltaTime;
    //    //
    //    //transform.LookAt(destination);
    //    //Bullet bullet = GetComponent<Bullet>();
    //    //bullet.SetDestination(target.position);
    //    //rb.velocity = player.transform.position * speed;
    //    //
    //    //if (remainTime < 0)
    //    //{
    //    //    pooledObject.ReturnToPool();
    //    //}
    //}
    //
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
    //        playerController.TakeHit();
    //    }
    //}
    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        destination = target.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(destination.normalized * Time.deltaTime * 100000);

        Destroy(gameObject, 3f);
    }
}
