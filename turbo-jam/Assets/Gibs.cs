using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gibs : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float acceleration;
    public float impulseForce;
    public Vector2 angularChange;

    public Vector2 Direction = Vector2.zero;

    static float t = 0.0f;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Direction = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        rb2d.AddForce(Direction * impulseForce, ForceMode2D.Impulse);
        float impulse = (Random.Range(angularChange.x,angularChange.y) * Mathf.Deg2Rad) * rb2d.inertia;
        rb2d.AddTorque(impulse, ForceMode2D.Impulse);

        Direction = Vector2.zero;
    }

    public void FixedUpdate()
    {
        Vector3 velocity = rb2d.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, Direction.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, Direction.y, maxSpeedChange);

        rb2d.angularVelocity = Mathf.Lerp(rb2d.angularVelocity, 0, t);
        t += 0.5f * Time.deltaTime;

        rb2d.velocity = velocity;
    }
}
