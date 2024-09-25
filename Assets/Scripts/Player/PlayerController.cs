using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlayerModel playerModel;

    [Header("State")]
    [SerializeField] IdleState idleState;
    [SerializeField] MoveState moveState;
    [SerializeField] JumpState jumpState;
    [SerializeField] DieState dieState;

    private void Awake()
    {
        
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
