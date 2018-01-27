using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Die : Photon.MonoBehaviour
{
    public Animator Anim;


    void Update()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
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
