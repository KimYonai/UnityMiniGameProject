using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Trace, Rush, Die, Size }
    [Header("Current State")]
    [SerializeField] BossState curState;

    [Header("Boss Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] PlayerController playerController;
    [SerializeField] SpriteRenderer bossRender;
    [SerializeField] Vector2 startPos;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool isTrace;
    [SerializeField] GameObject gameClear;

    [Header("Model")]
    [SerializeField] BossModel bossModel;

    private void Start()
    {
        bossModel.CurHP = bossModel.MaxHP;
        startPos = transform.position;
        gameClear.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            curState = BossState.Idle;
        }

        bossModel.RemainTime += Time.deltaTime;

        #region Boss State
        switch (curState)
        {
            case BossState.Idle:
                Idle();
                break;

            case BossState.Trace:
                Trace();
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
        else if (bossModel.CurHP == 0)
        {
            curState = BossState.Die;
        }
    }

    private void Trace()
    {
        if (player.transform.position.x > transform.position.x)
        {
            bossRender.flipX = true;
            rigid.velocity = Vector2.right * bossModel.MoveSpeed;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            bossRender.flipX = false;
            rigid.velocity = Vector2.left * bossModel.MoveSpeed;
        }

        if (Vector2.Distance(transform.position, player.transform.position) < 0.01f)
        {
            playerController.TakeHit();
        }

        if (Physics2D.OverlapCircle(transform.position, bossModel.TraceRange, playerLayer) == false)
        {
            curState = BossState.Idle;
        }
        else if (bossModel.RemainTime >= 5)
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
        bossModel.RemainTime = 0;

        if (player.transform.position.x > transform.position.x)
        {
            bossRender.flipX = true;
            rigid.velocity = Vector2.right * bossModel.RushSpeed;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            bossRender.flipX = false;
            rigid.velocity = Vector2.left * bossModel.RushSpeed;
        }

        if (bossModel.CurHP <= 0)
        {
            curState = BossState.Die;
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        gameClear.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.TakeHit();
            curState = BossState.Idle;
        }
        else if (collision != null)
        {
            curState = BossState.Idle;
        }     
    }
}