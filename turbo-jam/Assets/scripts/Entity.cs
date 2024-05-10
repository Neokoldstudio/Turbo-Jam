using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    private int healthPoints;
    public Rigidbody rb;
    public abstract void getHit(int Damage, Vector2 Direction);

    public abstract void hit();
}