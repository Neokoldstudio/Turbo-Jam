using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    bool isOpen = false;

    Animator anim;

    ParticleSystem particles;

    void OnValidate()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if(particles == null)
        {
            particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
    }


    public void OpenGate()
    {
        anim.SetTrigger("OpenGate");
        isOpen = true;
    }

    public void CloseGate()
    {
        anim.SetTrigger("CloseGate");
        isOpen = false;
    }

    public void StartParticles()
    {
        particles.Play();
    }
}
