using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : MonoBehaviour
{
    [SerializeField] int maxHP;
    public int MaxHP { get { return maxHP; } }

    [SerializeField] int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] float traceRange;
    public float TraceRange { get { return traceRange; } }

    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] float remainTime;
    public float RemainTime { get { return remainTime; } set { remainTime = value; } }

    [SerializeField] float rushSpeed;
    public float RushSpeed { get {return rushSpeed; } }
}
