using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Entity
{
    [SerializeField, Range(0f, 1000f)]
    private float ThrowForce;
    public override void getHit(int damage, Vector2 Direction)
    {
        Debug.Log(Direction);
        rb.AddForce(Direction.x * ThrowForce, Direction.y * ThrowForce, 0f, ForceMode.Impulse);
    }

    public override void hit()
    {}
}
