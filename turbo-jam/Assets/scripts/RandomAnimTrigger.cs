using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RandomAnimTrigger : SerializedMonoBehaviour
{
    public Animator anim;

    public string triggerName;

    public float refreshRate;
    [Range(0,100)]
    public float probability;

    Timer timer;

    private void Start()
    {
        timer = new Timer(refreshRate);   
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
