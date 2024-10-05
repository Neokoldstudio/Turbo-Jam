using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : Weapon
{
    [HideInInspector]
    public Animator sword_animation;

    private void Start()
    {
        if (sword_animation == null)
            sword_animation = GetComponentInParent<Animator>();

        if (wManager.animator != null)
        {
            wManager.animator.Rebind();
            wManager.animator.Update(0f);
        }
    }

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

    public override void OnHit(Entity target)
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

}
