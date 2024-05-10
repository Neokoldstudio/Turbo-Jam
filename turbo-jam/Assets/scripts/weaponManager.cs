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

    public void Attack()
    {
        if (weapon != null)
        {
            weapon.Attack();
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
        }
    }
}
