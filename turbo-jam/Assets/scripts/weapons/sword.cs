using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{

    public Animator sword_animation;

    [SerializeField, Range(0f, 10f)]
    public float vfxRange;

    public GameObject swingVfx;
    public GameObject impactVfx;
    public GameObject slashVfx;
    private Vector2 attackDir;

    public override bool Attack(Vector2 direction)
    {
        if(!sword_animation.GetBool("swing"))
        {
            sword_animation.SetTrigger("swing");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider hitCollider in hitColliders)
            {
                Entity entity = hitCollider.GetComponent<Entity>();
                if (entity != null)
                {
                    entity.getHit(damagePoint, direction);
                    SlashVfx(entity.transform);
                    ImpactVfx(entity.transform);
                }
            }
            attackDir = direction;
            return true;
        }
        return false;
    }

    public void SwingVfx()
    {
        GameObject swing = Instantiate(swingVfx);
        swing.transform.position = new Vector3(transform.position.x + attackDir.x * vfxRange, transform.position.y + attackDir.y * vfxRange, transform.position.z);
        swing.transform.rotation = Quaternion.LookRotation(Vector3.forward, attackDir);
        swing.transform.rotation *= Quaternion.Euler(0, 0, 90);
        swing.transform.localScale = transform.localScale;
    }

    public void ImpactVfx(Transform entityTransform)
    {
        GameObject impact = Instantiate(impactVfx);
        impact.transform.position = entityTransform.position;
    }

    public void SlashVfx(Transform entityTransform)
    {
        GameObject slash = Instantiate(slashVfx);
        slash.transform.position = entityTransform.position;
        slash.transform.rotation *= Quaternion.Euler(0,0,Random.Range(-180.0f, 180.0f));
    }

    public void Shake()
    {
        CinemachineShake.Instance.Shake(5f, .1f);
    }

    public override void Parry()
    {
        sword_animation.SetTrigger("parry");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oui");

        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            // Add the entity to the list of entities hit

            entity.getHit(damagePoint, attackDir);
        }
    }
}
