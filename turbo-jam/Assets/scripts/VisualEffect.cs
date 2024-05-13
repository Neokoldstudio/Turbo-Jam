using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffect : MonoBehaviour
{
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DestroyEffect()
    {
        Destroy(this.gameObject);
    }

}
