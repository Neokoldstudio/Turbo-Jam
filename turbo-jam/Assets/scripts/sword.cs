using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{

    public Animator sword_animation;

    public GameObject swingVfx;

    private Vector2 attackDir;
    public override void Attack(Vector2 direction)
    {
        sword_animation.SetTrigger("swing");
        attackDir = direction;
    }

    public void SwingVfx()
    {
        Instantiate(swingVfx);
    }

    public override void Parry()
    {
        sword_animation.SetTrigger("parry");
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            // Add the entity to the list of entities hit

            entity.getHit(damagePoint, attackDir);
        }
    }
}
