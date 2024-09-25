using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Run, Jump, Die, Size }
    [SerializeField] PlayerState curState = PlayerState.Idle;
    public BaseState[] states = new BaseState[(int)PlayerState.Size];

    [Header("Move Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] bool isMove;
    [SerializeField] bool isGrounded;

    [Header("Model")]
    [SerializeField] PlayerModel playerModel;

    [Header("State")]
    [SerializeField] IdleState idleState;
    [SerializeField] RunState runState;
    [SerializeField] JumpState jumpState;
    [SerializeField] DieState dieState;

    private void Awake()
    {
        states[(int)PlayerState.Idle] = idleState;
        states[(int)PlayerState.Run] = runState;
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

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        GroundCheck();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 run = new Vector2(x, 0) * playerModel.MoveSpeed;
        rigid.velocity = run;

        if (x < 0)
        {
            render.flipX = true;
        }
        else if (x > 0)
        {
            render.flipX = false;
        }

        if (rigid.velocity.x > 0.01f)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        if (rigid.velocity.x > playerModel.MaxMoveSpeed)
        {
            rigid.velocity = new Vector2(playerModel.MaxMoveSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -playerModel.MaxMoveSpeed)
        {
            rigid.velocity = new Vector2(-playerModel.MaxMoveSpeed, rigid.velocity.y);
        }

        if (rigid.velocity.y < -playerModel.MaxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -playerModel.MaxFallSpeed);
        }
    }

    public void Jump()
    {
        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * playerModel.JumpPower, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    [System.Serializable]
    private class IdleState : BaseState
    {
        
    }

    [System.Serializable]
    private class RunState : BaseState
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
