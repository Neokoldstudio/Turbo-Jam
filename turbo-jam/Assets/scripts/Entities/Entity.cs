using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Entity : SerializedMonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    private int healthPoints;
    public Rigidbody2D rb;
    public abstract void getHit(int Damage, Vector2 Direction);

    public abstract void hit();
    public virtual void parry(){}
}