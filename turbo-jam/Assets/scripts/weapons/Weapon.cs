using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    public int damagePoint;
    [SerializeField, Range(0f, 10f)]
    public float attackRange;

    [SerializeField, Range(0f, 10f)]
    public float parryStun;
    public abstract bool Attack(Vector2 direction);
    public abstract void Parry();
}