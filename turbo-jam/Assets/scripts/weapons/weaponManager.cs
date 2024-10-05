using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class weaponManager : SerializedMonoBehaviour
{
    public Weapon weapon;
    public bool canRotate = true;
    public bool perfectParry = false;
    private float rotationSpeed = 10f;

    public Animator animator;
    public RuntimeAnimatorController handsAnimation;

    [HideInInspector]
    public GameObject hands;
    [HideInInspector]
    public Color skinColor;

    public void Shake()
    {
        CinemachineShake.Instance.Shake(5f, .1f);
    }
    public void Swing()
    {
        if (weapon != null)
        {
            weapon.SwingVfx();
            weapon.GetComponent<SfxManager>().PlaySound("swing");
        }
    }
    public void Sparks()
    {
        if (weapon != null)
            weapon.SparkVfx();
    }
    public void PlaySFX(string sfxName)
    {
        if (weapon != null)
            weapon.GetComponent<SfxManager>().PlaySound(sfxName.ToLower());
    }
    public void UpdateRotation(Vector3 lookDirection)
    {
        if (canRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.localScale = new Vector3(Mathf.Sign(lookDirection.x), 1.0f, transform.localScale.z);
        }
    }

    public void FinishSwing()
    {
        canRotate = true;
    }

    public bool Attack(Vector2 Direction)
    {
        if (weapon != null)
        {
            canRotate = false;
            return weapon.Attack(Direction);
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
            return false;
        }
    }

    

    public void DropWeapon()
    {
        if (weapon.droppable)
        {
            Interactable weaponDrop = Instantiate(weapon.pickUpItem);
           
            weaponDrop.transform.position = transform.position - new Vector3(0, .25f, 0);
            weaponDrop.gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            weaponDrop.gameObject.transform.localScale = new Vector3(transform.localScale.x,1,1);
            (weaponDrop as ItemPickUp).dangerous = true;
        }

        UnEquipWeapon();
    }

    public void ThrowWeapon(float throwForcePercent, Vector2 throwDirection)
    {
        if (weapon.droppable)
        {
            Interactable weaponDrop = Instantiate(weapon.pickUpItem);
            weaponDrop.transform.position = transform.position + new Vector3(throwDirection.x/2, throwDirection.y / 2 - .1f, 0) ;
            weaponDrop.gameObject.transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(throwDirection.y,throwDirection.x)* Mathf.Rad2Deg)-90, Vector3.forward);
            weaponDrop.GetComponent<Rigidbody2D>().AddForce(throwDirection * weapon.throwForce * throwForcePercent, ForceMode2D.Impulse);
            (weaponDrop as ItemPickUp).dangerous = true;
            (weaponDrop as ItemPickUp).dangerousCollider.enabled = true;
        }
        UnEquipWeapon();
    }

    [Button("EquipWeapon")]
    public void EquipWeapon(Weapon item)
    {
        Weapon instantiatedWeapon = Instantiate(item.gameObject, this.transform).GetComponent<Weapon>();
        instantiatedWeapon.wManager = this;
        weapon = instantiatedWeapon;

        if (weapon.name.EndsWith("(Clone)"))
        {
            weapon.name = weapon.name.Substring(0, weapon.name.LastIndexOf("(Clone"));
        }
        animator.runtimeAnimatorController = instantiatedWeapon.animatorController;
        UpdateHandsVisuals();
    }
    void UnEquipWeapon()
    {
        Destroy(weapon.gameObject);
        weapon = null;
        animator.runtimeAnimatorController = null;
        UpdateHandsVisuals();
    }

    public void Parry()
    {
        if (weapon != null)
        {
            weapon.Parry();
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
        }
    }

    public void UpdateHandsVisuals()
    {
        if(weapon != null)
        {
            hands.SetActive(false);
            if(weapon.skinToRecolor.Length > 0)
            {
                foreach (SpriteRenderer sr in weapon.skinToRecolor)
                {
                    sr.color = skinColor;
                }
            }
        }
        else
        {
            hands.SetActive(true);
        }
    }

    public float getParryStun()
    {
        if (weapon != null)
        {
            return weapon.parryStun;
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
            return 1f;
        }
    }

    public bool getPerfectParryState()
    {
        return perfectParry;
    }

    public void SetPerfectParryTrue()
    {
        print(perfectParry);
        perfectParry = true;
    }

    public void SetPerfectParryFalse()
    {
        print(perfectParry);
        perfectParry = false;
    }
}
