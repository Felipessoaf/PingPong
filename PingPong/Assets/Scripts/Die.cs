using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Die : NetworkBehaviour
{
    public Animator Anim;


    void Update()
    {
        if (!isLocalPlayer) return;
    }

    public void Death()
    {
        if(Anim)
        {
            Anim.SetTrigger("Death");
        }
        else
        {
            Debug.LogWarning("Animator not set - Die.cs");
        }
        SendMessage("PlayerDied");
    }

    public void PlayerDied()
    {
        Debug.Log("PlayerDied");
    }
}
