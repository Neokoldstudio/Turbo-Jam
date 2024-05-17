using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = transform.GetChild(0).GetComponent<Weapon>();
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
