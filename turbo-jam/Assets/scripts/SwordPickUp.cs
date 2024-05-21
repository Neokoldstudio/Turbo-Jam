using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    public Collider2D col;

    Renderer rend;
    public AnimationController anim;

    public bool playerThere;

    public void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.PickSword();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            rend.sharedMaterial.SetFloat("_Outline_Tickness", 1);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            rend.sharedMaterial.SetFloat("_Outline_Tickness", 0);
    }
}
