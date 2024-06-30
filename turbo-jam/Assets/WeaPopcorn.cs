using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaPopcorn : Weapon
{
    public int maxAmmo;
    int ammo;

    public Animator anim;
    public SpriteRenderer popcornBagSR;

    public Sprite bagFull;
    public Sprite bagHalf;
    public Sprite bagEmpty;
    bool empty = false;

    private void Start()
    {
        if(anim == null)
            anim = GetComponentInParent<Animator>();

        ammo = maxAmmo;
    }
    public override bool Attack(Vector2 direction)
    {
        attackDir = direction;
        if(!empty && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PopcornAttack")
        {
            if (!anim.GetBool("swing"))
            {
                anim.SetTrigger("swing");
            }
            if (ammo == 0)
            {

                popcornBagSR.sprite = bagEmpty;
                empty = true;
            }
            else if (ammo < maxAmmo/2)
            {
                popcornBagSR.sprite = bagHalf;
            }
            else
            {
                popcornBagSR.sprite = bagFull;
            }
            ammo--;
            return true;
        }
        return false;
    }

    public override void Parry()
    {
        anim.SetTrigger("parry");
    }

    public override void SwingVfx()
    {
        int swing = vfxManager.TriggerVfx(VfxType.Swing);
        if (swing != -1)
        {

            vfxManager.currentVfx[swing].transform.position = new Vector3(transform.position.x + attackDir.x * vfxRange, transform.position.y + attackDir.y * vfxRange, transform.position.z);
            vfxManager.currentVfx[swing].transform.rotation = Quaternion.LookRotation(Vector3.forward, attackDir);
            //vfxManager.currentVfx[swing].transform.rotation *= Quaternion.Euler(0, 0, -90);//rotate 90 degrees to align the swing effect with the weapon
        }
    }
    public override void OnHit(Entity target)
    {
        if (target != null)
        {
            target.getHit(damagePoint, attackDir);
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(attackDir.x * knockbackForce, attackDir.y * knockbackForce), ForceMode2D.Impulse);
            ImpactVfx(target.transform);
        }
    }
}
