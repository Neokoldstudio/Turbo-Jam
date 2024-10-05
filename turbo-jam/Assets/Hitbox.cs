using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    Collider2D col;

    public string targetTag;

    public Weapon weapon;

    [HideInInspector]
    public bool hitSomething;

    [HideInInspector]
    public Vector2 hitpoint;

    int penetrationIndex;

    private void OnEnable()
    {
        penetrationIndex = weapon.penetration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag(targetTag) && penetrationIndex > 0)
            {
                hitSomething = true;
                hitpoint = collision.bounds.center;
                weapon.OnHit(collision.gameObject.GetComponent<Entity>());
                penetrationIndex--;
            }
        }
    }
    private void OnDisable()
    {
        penetrationIndex = 0;
        hitSomething = false;
        hitpoint = Vector3.zero;
    }

}
