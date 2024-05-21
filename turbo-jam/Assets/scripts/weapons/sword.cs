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
        if (!sword_animation.GetBool("swing"))
        {
            sword_animation.SetTrigger("swing");
            attackDir = direction;
            return true;
        }
        return false;
    }

    public override void Parry()
    {
        sword_animation.SetTrigger("parry");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Armadillo")
        {
            //just for intro animation
            AnimationController animController = AnimationController.Instance;
            animController.KillArmadillo();
            SlashVfx(animController.armadillo.gameObject.transform);
            ImpactVfx(animController.armadillo.gameObject.transform);
        }
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            // Add the entity to the list of entities hit
            entity.getHit(damagePoint, attackDir);
            Rigidbody2D rb = entity.GetComponent<Rigidbody2D>(); 
            rb.AddForce(new Vector2(attackDir.x * knockbackForce, attackDir.y * knockbackForce), ForceMode2D.Impulse);
            SlashVfx(entity.transform);
            ImpactVfx(entity.transform);
        }
    }


    #region VFX

    public void SwingVfx()
    {
        GameObject swing = Instantiate(swingVfx);
        swing.transform.position = new Vector3(transform.position.x + attackDir.x * vfxRange, transform.position.y + attackDir.y * vfxRange, transform.position.z);
        swing.transform.rotation = Quaternion.LookRotation(Vector3.forward, attackDir);
        swing.transform.rotation *= Quaternion.Euler(0, 0, 90);//rotate 90 degrees to align the swing effect with the weapon
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
    #endregion
}
