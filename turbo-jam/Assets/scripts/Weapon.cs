using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    public int damagePoint;
    [SerializeField, Range(0f, 10f)]
    public float attackRange;
    public abstract void Attack(Vector2 direction);
}
