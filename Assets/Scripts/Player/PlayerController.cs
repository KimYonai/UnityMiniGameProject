using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Run, Jump, Die }
    [Header("Current State")]
    [SerializeField] PlayerState curState;

    [Header("Player Settings")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject gameOver;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    public SpriteRenderer Render { get { return render; } }
    [SerializeField] SpriteRenderer bulletRender;
    [SerializeField] ParticleSystem playerDeadParticle;
    [SerializeField] float fireTime;
    [SerializeField] float remainTime;
    [SerializeField] bool isMove;
    [SerializeField] bool isGrounded;

    [Header("Animation")]
    [SerializeField] Animator animator;
    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");
    private int curAniHash;

    [Header("Model")]
    [SerializeField] PlayerModel playerModel;

    private GameManager gameManager;

    private void Start()
    {
        curState = PlayerState.Idle;
        gameManager = GetComponent<GameManager>();
        playerModel.CurHP = playerModel.MaxHP;
        remainTime = fireTime;
        
        gameOver.SetActive(false);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void Update()
    {
        remainTime -= Time.deltaTime;
        playerDeadParticle.transform.position = transform.position;

        #region State Switch
        switch (curState)
        {
            case PlayerState.Idle:
                Idle();
                break;

            case PlayerState.Run:
                Run();
                break;

            case PlayerState.Jump:
                Jump();
                break;

            case PlayerState.Die:
                Die();
                break;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerShoot();
        }

        GroundCheck();

        AnimationPlay();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 run = new Vector2(x * playerModel.MoveSpeed, rigid.velocity.y);
        rigid.velocity = run;

        if (x < 0)
        {
            render.flipX = true;
        }
        else if (x > 0)
        {
            render.flipX = false;
        }

        if (rigid.velocity.x > 0.01f || rigid.velocity.x < -0.01f)
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
    }

    private void PlayerJump()
    {
        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * playerModel.JumpPower, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        Debug.DrawRay(rigid.position, Vector2.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

   private void PlayerShoot()
   {
       
   
       if (remainTime <= 0)
       {
            Vector3 dir = transform.right * transform.localScale.x;

            if (render.flipX == false)
            {
                bulletRender.flipX = false;
                Instantiate(bulletObj, transform.position + dir, Quaternion.identity);
            }
            else
            {
                bulletRender.flipX = true;
                Instantiate(bulletObj, transform.position - dir, Quaternion.identity);
            }
            
            remainTime = fireTime;
       }
   }

    public void TakeHit()
    {
        playerModel.CurHP--;
    }

    private void AnimationPlay()
    {
        int checkAniHash;

        if (rigid.velocity.y > 0.01f)
        {
            checkAniHash = jumpHash;
        }
        else if (rigid.velocity.y < -0.01f)
        {
            checkAniHash = fallHash;
        }
        else if (rigid.velocity.sqrMagnitude < 0.01f)
        {
            checkAniHash = idleHash;
        }
        else
        {
            checkAniHash = runHash;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }

    private void Idle()
    {
        if (isMove == true)
        {
            curState = PlayerState.Run;
        }
        else if (isGrounded == false)
        {
            curState = PlayerState.Jump;
        }
        else if (playerModel.CurHP <= 0)
        {
            curState = PlayerState.Die;
        }
    }

    private void Run()
    {
        if (isMove == false)
        {
            curState = PlayerState.Idle;
        }
        else if (isGrounded == false)
        {
            curState = PlayerState.Jump;
        }
        else if (playerModel.CurHP <= 0)
        {
            curState = PlayerState.Die;
        }
    }

    private void Jump()
    {
        if (isGrounded == true)
        {
            if (isMove == false)
            {
                curState = PlayerState.Idle;
            }
            else if (isMove == true)
            {
                curState = PlayerState.Run;
            }
        }
        else if (playerModel.CurHP <= 0)
        {
            curState = PlayerState.Die;
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        playerDeadParticle.Play();
        gameOver.SetActive(true);
    }
}
