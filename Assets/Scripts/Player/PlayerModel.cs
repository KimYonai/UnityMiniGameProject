using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] int maxHP;
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }

    [SerializeField] int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] float maxMoveSpeed;
    public float MaxMoveSpeed { get { return maxMoveSpeed; } }

    [SerializeField] float jumpPower;
    public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }

    [SerializeField] float maxFallSpeed;
    public float MaxFallSpeed { get { return maxFallSpeed; } }
}
