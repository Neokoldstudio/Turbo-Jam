using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCallbackHook : MonoBehaviour
{

    public void PlayerEndDodge()
    {
        PlayerMovement Player = transform.parent.gameObject.GetComponent<PlayerMovement>();

        Player.StopDodge();
    }

    public void PlayerEnableDamage()
    {

    }
}
