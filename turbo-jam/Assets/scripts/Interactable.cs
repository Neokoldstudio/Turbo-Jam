using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public bool triggerOnTouch = false;

    public List<Entity> interacters;

    public Renderer rend;

    public void Start()
    {
        RemoveHighLight();
    }

    public void HighLight()
    {
        rend.material.SetFloat("_Outline_Tickness", 1);
    }
    public void RemoveHighLight()
    {
        rend.material.SetFloat("_Outline_Tickness", 0);
    }

    public virtual void OnInteract(Entity interacter)
    {

    }
}
