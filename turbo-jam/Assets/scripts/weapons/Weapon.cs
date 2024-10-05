using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Weapon : SerializedMonoBehaviour
{
    [SerializeField, Range(0, 10)]
    public int damagePoint;
    
    [SerializeField, Range(1,6)]
    public int penetration = 1;

    [Range(0, 2)]
    public float throwChargeTime;
    public float throwForce;

    [SerializeField, Range(0f, 10f)]
    public float attackRange;

    [SerializeField, Range(0f, 10f)]
    public float parryStun;

    [SerializeField, Range(0f, 100f)]
    public float knockbackForce;
    private bool IsAttacking{get;set;}
    public LayerMask enemyLayer;

    public bool droppable;

    [ShowIf("droppable")]
    public ItemPickUp pickUpItem;

    public bool requiresAnimation;

    [ShowIf("requiresAnimation")]
    public RuntimeAnimatorController animatorController;

    public SpriteRenderer[] skinToRecolor;

    public VfxManager vfxManager;
    [SerializeField, Range(0f, 10f)]
    public float vfxRange;
    [HideInInspector]
    public Vector2 attackDir;

    [HideInInspector]
    public weaponManager wManager;

    public virtual bool Attack(Vector2 direction)
    {
        return false;
    }


    public virtual void Parry() 
    { 

    }
        
    public virtual void OnHit(Entity target)
    {
        wManager.FinishSwing();
    }

    #region VFX

    public virtual void SwingVfx()
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
        if (impact != -1)
        {
            vfxManager.currentVfx[impact].transform.position = entityTransform.position;
        }
    }

    public void SlashVfx(Transform entityTransform)
    {
        int slash = vfxManager.TriggerVfx(VfxType.Slash);
        if (slash != -1)
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
