using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Range(0f, 1000f)]
    private float damagePoint;
    public void Attack()
    {
        Debug.Log("AH");
    }
}
