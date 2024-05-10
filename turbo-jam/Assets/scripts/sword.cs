using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{
    public override void Attack(Vector2 direction)
    {

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
}
