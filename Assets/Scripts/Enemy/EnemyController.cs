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
    [SerializeField] ObjectPool[] bulletPool;
    private ObjectPool curBulletPool;
    [SerializeField] Vector2 startPos;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool isTrace;

    [Header("Animation")]
    [SerializeField] Animator animator;
    private static int idleHash = Animator.StringToHash("Idle");
    private static int AttackHash = Animator.StringToHash("Jump");
    private int curAniHash;

    [Header("Model")]
    [SerializeField] EnemyModel enemyModel;

    private void Start()
    {
        enemyModel.CurHP = enemyModel.MaxHP;
        curBulletPool = bulletPool[0];
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
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

    private void AnimationPlay()
    {
        int checkAniHash;

        if (curState == EnemyState.Attack)
        {
            checkAniHash = AttackHash;
        }
        else
        {
            checkAniHash = idleHash;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }

    private void Idle()
    {
        if (Physics2D.OverlapCircle(transform.position, enemyModel.TraceRange, playerLayer) == true)
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
        curBulletPool.GetPool(transform.position, transform.rotation);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
