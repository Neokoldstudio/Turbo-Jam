using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public CanvasAnimation canvasAnim;

    Animator anim;

    private void OnValidate()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void ShowMenu()
    {
        canvasAnim.gameObject.SetActive(true);
    }

    public void HideTitle()
    {
        canvasAnim.HideTitle();
    }

    public void DropPenguin()
    {
        anim.SetTrigger("DropPenguin");
    }
}
