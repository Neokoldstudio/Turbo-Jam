using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public CanvasAnimation canvasAnim;
    public GameObject menu;

    Animator anim;

    private void OnValidate()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void ShowMenu()
    {
        canvasAnim.ShowTitle();
        menu.SetActive(true);
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
