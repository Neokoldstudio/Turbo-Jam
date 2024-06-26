using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class ParticleEffectGravity : MonoBehaviour
{
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        Debug.Log(Mathf.Abs(transform.rotation.z));
        main.startSpeed = remap(Mathf.Abs(transform.rotation.z), 0, 1, main.startSpeed.constant, 0);
        main.startLifetime = remap(Mathf.Abs(transform.rotation.z), 0, 1, main.startLifetime.constant, .6f);
        Debug.Log(main.startSpeed.constant);
    }

    public static float remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}
