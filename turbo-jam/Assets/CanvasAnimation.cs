using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
    Animator anim;

    private void OnValidate()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void HideTitle()
    {
        anim.SetTrigger("HideTitle");
    }
}
