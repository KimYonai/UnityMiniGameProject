using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static EnemyController;

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Trace, Charge, Rush, Die, Size }
    [Header("Current State")]
    [SerializeField] BossState curState;

    [Header("Enemy Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector2 startPos;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool isTrace;

    [Header("Model")]
    [SerializeField] BossModel bossModel;

    private void Start()
    {
        bossModel.CurHP = bossModel.MaxHP;
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            curState = BossState.Idle;
        }

        bossModel.RemainTime -= Time.deltaTime;

        #region Boss State
        switch (curState)
        {
            case BossState.Idle:
                Idle();
                break;

            case BossState.Trace:
                Trace();
                break;

            case BossState.Charge:
                Charge();
                break;

            case BossState.Rush:
                Rush();
                break;

            case BossState.Die:
                Die();
                break;
        }
        #endregion 
    }

    public void TakeHit()
    {
        bossModel.CurHP--;
    }

    private void Idle()
    {
        if (Physics2D.OverlapCircle(transform.position, bossModel.TraceRange, playerLayer) == true)
        {
            curState = BossState.Trace;
        }
        else if (isTrace == true && bossModel.RemainTime <= 0)
        {
            curState = BossState.Charge;
        }
        else if (bossModel.CurHP == 0)
        {
            curState = BossState.Die;
        }
    }

    private void Trace()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossModel.MoveSpeed * Time.deltaTime);

        if (Physics2D.OverlapCircle(transform.position, bossModel.TraceRange, playerLayer) == false)
        {
            curState = BossState.Idle;
        }
        else if (isTrace == true && bossModel.RemainTime <= 0)
        {
            curState = BossState.Charge;
        }
        else if (bossModel.CurHP <= 0)
        {
            curState = BossState.Die;
        }
    }

    private void Charge()
    {
        bossModel.ChargeTime += Time.deltaTime;
        float dir = player.GetComponent<Transform>().position.x < transform.position.x ? -1 : 1;
        transform.localScale = new Vector2(dir * -1, 0);

        if (bossModel.ChargeTime >= 1)
        {
            curState = BossState.Rush;
        }
        else if (bossModel.CurHP <= 0)
        {
            curState = BossState.Die;
        }
    }

    private void Rush()
    {
       //rigid.velocity = new Vector2(player.transform.position.x, 0) * bossModel.RushSpeed;
       //
       //if (player.transform.position.x < 0)
       //{
       //    gameObject.render.flipX
       //}
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
