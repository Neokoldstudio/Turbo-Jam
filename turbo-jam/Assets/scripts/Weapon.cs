using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Range(0, 1000)]
    private int damagePoint;
    [SerializeField, Range(0f, 10f)]
    private float attackRange = 0.8f;
    public void Attack(Vector2 direction)
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
