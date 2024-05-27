using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    private Weapon weapon;
    public GameObject hitbox;

    public bool canRotate = true;
    private float rotationSpeed = 10f;

    private void Start()
    {
        weapon = transform.GetChild(0).GetComponent<Weapon>();
    }

    public void Shake()
    {
        CinemachineShake.Instance.Shake(5f, .1f);
    }
    public void Swing()
    {
        (weapon as sword).SwingVfx();
    }
    public void Sparks()
    {
        (weapon as sword).SparkVfx();
    }


    public void SetActiveHitBox(bool value)
    {
        hitbox.SetActive(value);
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
}
