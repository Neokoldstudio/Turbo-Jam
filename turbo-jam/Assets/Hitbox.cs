using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    Collider2D col;

    public string targetTag;

    public sword sword;

    public bool hitSomething;

    public Vector2 hitpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag(targetTag))
                sword.OnHit(collision.GetComponent<Entity>());
        }
    }

}
