using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimTrigger : MonoBehaviour
{
    public Animator anim;

    public string triggerName;

    public float probability;

    Timer timer;

    private void Start()
    {
        timer = new Timer(1);   
    }

    private void Update()
    {
        if (timer.IsOverLoop())
        {
            float randomFloat = Random.Range(0, 100);
            if (randomFloat < probability)
                anim.SetTrigger(triggerName);
        }
    }
}
