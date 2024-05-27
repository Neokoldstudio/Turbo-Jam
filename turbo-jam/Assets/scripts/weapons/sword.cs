using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{

    public Animator sword_animation;
    public VfxManager vfxManager;

    [SerializeField, Range(0f, 10f)]
    public float vfxRange;
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

    public void OnHit(Entity target)
    {
        if (target.tag == "Armadillo")
        {
            //just for intro animation
            AnimationController animController = AnimationController.Instance;
            animController.KillArmadillo();
            SlashVfx(animController.armadillo.gameObject.transform);
            ImpactVfx(animController.armadillo.gameObject.transform);
        }

        if (target != null)
        {
            // Add the entity to the list of entities hit
            target.getHit(damagePoint, attackDir);
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(attackDir.x * knockbackForce, attackDir.y * knockbackForce), ForceMode2D.Impulse);
            SlashVfx(target.transform);
            ImpactVfx(target.transform);
        }
    }

    #region VFX

    public void SwingVfx()
    {
        int swing = vfxManager.TriggerVfx(VfxType.Swing);
        if (swing != -1)
        {

            vfxManager.currentVfx[swing].transform.position = new Vector3(transform.position.x + attackDir.x * vfxRange, transform.position.y + attackDir.y * vfxRange, transform.position.z);
            vfxManager.currentVfx[swing].transform.rotation = Quaternion.LookRotation(Vector3.forward, attackDir);
            vfxManager.currentVfx[swing].transform.rotation *= Quaternion.Euler(0, 0, 90);//rotate 90 degrees to align the swing effect with the weapon
        }
    }

    public void ImpactVfx(Transform entityTransform)
    {
        int impact = vfxManager.TriggerVfx(VfxType.Impact);
        if(impact != -1)
        {
            vfxManager.currentVfx[impact].transform.position = entityTransform.position;
        }
    }

    public void SlashVfx(Transform entityTransform)
    {
        int slash = vfxManager.TriggerVfx(VfxType.Slash);
        if(slash != -1)
        {
            vfxManager.currentVfx[slash].transform.position = entityTransform.position;
            vfxManager.currentVfx[slash].transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f));
        }
    }

    public void SparkVfx()
    {
        int spark = vfxManager.TriggerVfx(VfxType.Sparks);
    }
    #endregion
}
