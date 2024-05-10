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

    public void Attack(Vector2 Direction)
    {
        if (weapon != null)
        {
            weapon.Attack(Direction);
        }
        else
        {
            Debug.LogWarning("Weapon reference is not set!");
        }
    }
}
