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
        Vector2 throwForce = new Vector2(Direction.x * ThrowForce, Direction.y * ThrowForce);
        rb.AddForce(throwForce, ForceMode2D.Impulse);
    }

    public override void hit()
    {}
}
