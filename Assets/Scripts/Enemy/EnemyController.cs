using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, Die, Size }
    [Header("Current State")]
    [SerializeField] EnemyState curState;

    [Header("Enemy Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject bulletObj;
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector2 startPos;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float fireTime;
    [SerializeField] float remainTime;
    [SerializeField] bool isTrace;

    [Header("Model")]
    [SerializeField] EnemyModel enemyModel;

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        remainTime = fireTime;

    }

    private void Update()
    {
        if (player == null)
        {
            curState = EnemyState.Idle;
        }

        #region Enemy State
        switch (curState)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Trace:
                Trace(); 
                break;

            case EnemyState.Attack:
                Attack(); 
                break;

            case EnemyState.Die:
                Die();
                break;
        }
        #endregion   
    }

    public void TakeHit()
    {
        enemyModel.CurHP--;
    }

    private void Idle()
    {
        if (Physics2D.OverlapCircle(transform.position, 5, playerLayer) == true)
        {
            curState = EnemyState.Trace;
        }
        else if (enemyModel.AttackRange > 1)
        {
            curState = EnemyState.Attack;
        }
        else if (enemyModel.CurHP <= 0)
        {
            curState = EnemyState.Die;
        }
    }

    private void Trace()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyModel.MoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.transform.position) < 0.01f)
        {
            playerController.TakeHit();
        }

        if (Physics2D.OverlapCircle(transform.position, 5, playerLayer) == false)
        {
            curState = EnemyState.Idle;
        }
        else if (enemyModel.CurHP <= 0)
        {
            curState = EnemyState.Die;
        }
    }

    private void Attack()
    {
        remainTime -= Time.deltaTime;

        if (remainTime <= 0)
        {
            GameObject bulletGameObject = Instantiate(bulletObj, transform.position, transform.rotation);

            remainTime = fireTime;
        }

        if (enemyModel.CurHP <= 0)
        {
            curState = EnemyState.Die;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
