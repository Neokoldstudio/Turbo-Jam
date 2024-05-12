using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{

    public Animator sword_animation;
    public override void Attack(Vector2 direction)
    {
        sword_animation.SetTrigger("swing");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            Entity entity = hitCollider.GetComponent<Entity>();
            if (entity != null)
            {
                entity.getHit(damagePoint, direction);
            }
        }
    }

    public override void Parry()
    {
        sword_animation.SetTrigger("parry");
    }
}
