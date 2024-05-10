using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField, Range(0f, 1000f)]
    private float healthPoints;
    private void getHit(int Damage){}
}