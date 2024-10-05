using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Collider2D catchHitBox;

    public bool dangerous;

    public float height;
    public GameObject item;

    public AnimationCurve throwCurve;

    float time;
    float speed = 1;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (dangerous)
        {
            float t = time / speed;
            height = throwCurve.Evaluate(t);

            time += Time.fixedDeltaTime;
            item.transform.position = transform.position + new Vector3(0, height, 0);

            rb2d.drag = (height < 0.05f) ? 5 : 0.1f;
        } 
    }
}
