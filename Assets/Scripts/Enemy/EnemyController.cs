using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Return, Attack, Die, Size }
    [Header("Current State")]
    [SerializeField] EnemyState curState;
    private BaseState[] states = new BaseState[(int)EnemyState.Size];

    [Header("Enemy Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Vector2 startPos;
    [SerializeField] float fireTime;
    [SerializeField] float remainTime;
    [SerializeField] bool isTrace;

    [Header("State Settings")]
    [SerializeField] IdleState idleState;
    [SerializeField] AttackState attackState;

    [Header("Model")]
    [SerializeField] EnemyModel enemyModel;

    private void Awake()
    {
        states[(int)EnemyState.Idle] = idleState;
        states[(int)EnemyState.Attack] = attackState;
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
            if (enemy.target.transform.position != null)
            {
                enemy.ChangeState(EnemyState.Attack);
            }
            else if (enemy.player == null)
            {
                enemy.ChangeState(EnemyState.Idle);
            }
        }
    }

    [System.Serializable]
    private class AttackState : BaseState
    {
        [SerializeField] EnemyController enemy;

        public override void Update()
        {
            if (enemy.player == null)
            {
                enemy.ChangeState(EnemyState.Idle);
            }
        }
    }
}
