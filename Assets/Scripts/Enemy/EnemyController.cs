using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Return, Attack, Die, Size }
    [Header("Current State")]
    [SerializeField] EnemyState curState;
    private BaseState[] states = new BaseState[(int)EnemyState.Size];

    [Header("Move Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Vector2 startPos;
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
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    [System.Serializable]
    private class IdleState : BaseState
    {

    }

    [System.Serializable]
    private class TraceState : BaseState
    {

    }

    [System.Serializable]
    private class ReturnState : BaseState
    {

    }

    [System.Serializable]
    private class AttackState : BaseState
    {

    }

    [System.Serializable]
    private class DieState : BaseState
    {

    }
}
