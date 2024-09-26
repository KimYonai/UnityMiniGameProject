using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] int maxHP;
    public int MaxHP { get { return maxHP; } }

    [SerializeField] int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; OnHPChanged?.Invoke(curHP); } }
    public UnityAction<int> OnHPChanged;

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] float traceRange;
    public float TraceRange { get { return traceRange; } }

    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] float attackPoint;
    public float AttackPoint { get { return attackPoint; } }
}
