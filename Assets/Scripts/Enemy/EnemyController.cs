using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Return, Attack, Die, Size }
    [Header("Current State")]
    [SerializeField] EnemyState curState;
    private BaseState[] states = new BaseState[(int)EnemyState.Size];

    [Header("Enemy Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Vector2 startPos;
    [SerializeField] float fireTime;
    [SerializeField] float remainTime;
    [SerializeField] bool isTrace;

    [Header("State Settings")]
    [SerializeField] IdleState idleState;
    [SerializeField] TraceState traceState;
    [SerializeField] ReturnState returnState;
    [SerializeField] AttackState attackState;
    [SerializeField] DieState dieState;

    [Header("Model")]
    [SerializeField] EnemyModel enemyModel;

    private void Awake()
    {
        states[(int)EnemyState.Idle] = idleState;
        states[(int)EnemyState.Trace] = traceState;
        states[(int)EnemyState.Return] = returnState;
        states[(int)EnemyState.Attack] = attackState;
        states[(int)EnemyState.Die] = dieState;
    }

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        remainTime = fireTime;
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();

        remainTime -= Time.deltaTime;

        if (remainTime <= 0)
        {
            if (curState == EnemyState.Attack)
            {
                GameObject bulletGameObject = Instantiate(bulletObj, transform.position, transform.rotation);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                bullet.SetDestination(player.transform.position);

                remainTime = fireTime;
            }
        }
    }
    
    public void ChangeState(EnemyState nextState)
    {
        states[(int)curState].Exit();
        curState = nextState;
        states[(int)curState].Enter();
    }

    [System.Serializable]
    private class IdleState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.enemyModel.TraceRange)
            {
                enemy.ChangeState(EnemyState.Trace);
            }
            else if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.enemyModel.AttackRange)
            {
                enemy.ChangeState(EnemyState.Attack);
            }
            else if (enemy.enemyModel.CurHP <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    [System.Serializable]
    private class TraceState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.transform.position, enemy.enemyModel.MoveSpeed * Time.deltaTime);

            if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.enemyModel.TraceRange)
            {
                enemy.ChangeState(EnemyState.Return);
            }
            else if (enemy.enemyModel.CurHP <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    [System.Serializable]
    private class ReturnState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.startPos, enemy.enemyModel.ReturnSpeed * Time.deltaTime);

            if (Vector2.Distance(enemy.transform.position, enemy.startPos) < 0.01f)
            {
                enemy.ChangeState(EnemyState.Idle);
            }
            else if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.enemyModel.TraceRange)
            {
                enemy.ChangeState(EnemyState.Trace);
            }
        }
    }

    [System.Serializable]
    private class AttackState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            if (Vector2.Distance(enemy.transform.position, enemy.startPos) < enemy.enemyModel.AttackRange)
            {
                enemy.ChangeState(EnemyState.Idle);
            }
        }
    }

    [System.Serializable]
    private class DieState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            Destroy(enemy.gameObject);
        }
    }
}
