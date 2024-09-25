using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Move, Jump, Die, Size }
    [SerializeField] PlayerState curState = PlayerState.Idle;
    public BaseState[] states = new BaseState[(int)PlayerState.Size];

    [Header("Model")]
    [SerializeField] PlayerModel playerModel;

    [Header("State")]
    [SerializeField] IdleState idleState;
    [SerializeField] MoveState moveState;
    [SerializeField] JumpState jumpState;
    [SerializeField] DieState dieState;

    private void Awake()
    {
        states[(int)PlayerState.Idle] = idleState;
        states[(int)PlayerState.Move] = moveState;
        states[(int)PlayerState.Jump] = jumpState;
        states[(int)PlayerState.Die] = dieState;
    }

    private void Start()
    {
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        
    }

    [System.Serializable]
    private class IdleState : BaseState
    {

    }

    [System.Serializable]
    private class MoveState : BaseState
    {

    }

    [System.Serializable]
    private class JumpState : BaseState
    {

    }

    [System.Serializable]
    private class DieState : BaseState
    {

    }
}
