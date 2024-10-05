using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Cactus : Entity
{
    [SerializeField, Range(0f, 50f)]
    private float ThrowForce;

    public bool _throwRagdoll;

    [ShowIf("_throwRagdoll")]
    public GameObject choppedCactusObject;
    [ShowIf("_throwRagdoll")]
    public SpriteRenderer choppedCactusRenderer;
    [ShowIf("_throwRagdoll")]
    public Sprite choppedCactusSprite;
    public override void getHit(int damage, Vector2 Direction)
    {
        Debug.Log(Direction);
        if (_throwRagdoll)
        {
            choppedCactusRenderer.sprite = choppedCactusSprite;
            choppedCactusObject.SetActive(true);
            Vector2 throwForce = new Vector2(Direction.x * ThrowForce, Direction.y * ThrowForce);
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            _throwRagdoll = false;
        }
    }

    public override void hit()
    {}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
