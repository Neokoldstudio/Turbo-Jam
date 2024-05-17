using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    private int healthPoints;
    public Rigidbody2D rb;
    public abstract void getHit(int Damage, Vector2 Direction);

    [SerializeField] AudioClip SFX_hit;
    [SerializeField] AudioClip SFX_death;
    [SerializeField] AudioClip SFX_attack;

    public abstract void hit();
    public virtual void parry(){}
}