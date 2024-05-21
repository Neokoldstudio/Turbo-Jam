using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAnimation : MonoBehaviour { 
    public UnityEvent eventAnim;

    public void TrigAnim()
    {
        eventAnim.Invoke();
    }
}
