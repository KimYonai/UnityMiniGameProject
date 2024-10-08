using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] int maxHP;
    public int MaxHP { get { return maxHP; } }

    [SerializeField] int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] float returnSpeed;
    public float ReturnSpeed { get { return returnSpeed; } }

    [SerializeField] float bulletSpeed;
    public float BulletSpeed { get {return bulletSpeed; } }

    [SerializeField] float traceRange;
    public float TraceRange { get { return traceRange; } }

    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }
}
