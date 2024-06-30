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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag(targetTag))
            {
                hitSomething = true;
                hitpoint = collision.bounds.center;
                weapon.OnHit(collision.GetComponent<Entity>());
            }
        }
    }
    private void OnDisable()
    {
        hitSomething = false;
        hitpoint = Vector3.zero;
    }

}
