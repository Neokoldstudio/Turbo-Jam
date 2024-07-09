using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    public Weapon weapon;
    public bool canRotate = true;
    public bool perfectParry = false;
    private float rotationSpeed = 10f;

    public void Shake()
    {
        CinemachineShake.Instance.Shake(5f, .1f);
    }
    public void Swing()
    {
        if (weapon != null)
            weapon.SwingVfx();
    }
    public void Sparks()
    {
        if (weapon != null)
            weapon.SparkVfx();
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

    public bool Attack(Vector2 Direction)
    {
        if (weapon != null)
        {
            return weapon.Attack(Direction);
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
            return false;
        }
    }

    public void DropWeapon(bool throwWeapon)
    {

    }

    void UnEquipWeapon()
    {
        weapon = null;
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
