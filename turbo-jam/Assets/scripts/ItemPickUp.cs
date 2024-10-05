using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable 
{
    public Collider2D dangerousCollider;

    public Weapon weapon;

    [HideInInspector]
    public bool dangerous = false;

    public override void OnInteract(Entity interacter)
    {
        (interacter as PlayerMovement).GrabItem(this);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if(entity != null)
        {
            if (triggerOnTouch)
            {
                OnInteract(entity);
            }
            interacters.Add(entity);
            entity.interactables.Add(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (dangerous)
        {
            if (entity != null)
            {
                float entitySpriteHeight = entity.bodySprite.sprite.bounds.size.y;
                Debug.LogWarning(entitySpriteHeight);
                float currentHeight = GetComponent<Throwable>().height; Debug.LogWarning(currentHeight);
                if (currentHeight < entitySpriteHeight)
                {
                    transform.parent = entity.transform;
                    GetComponent<Throwable>().dangerous = false;
                }
                else
                {
                    return;
                }
            }

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            dangerous = false;
            dangerousCollider.enabled = false;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            if (interacters.Contains(entity))
                entity.interactables.Remove(this);
                interacters.Remove(entity);
        }
        if(interacters.Count == 0)
        {
            RemoveHighLight();
        }
    }
    
    private void OnDestroy()
    {
        foreach(Entity interacter in interacters)
        {
            interacter.interactables.Remove(this);
        }
        interacters.Clear();
    }
}
