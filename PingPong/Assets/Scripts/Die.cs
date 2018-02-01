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
            Anim.SetTrigger("death");
        }
        else
        {
            Debug.LogWarning("Animator not set - Die.cs");
        }
        //photonView.RPC("PlayerDied", PhotonTargets.All);

        //bool lastAlive = true;
        //foreach(GameObject go in GameObject.FindGameObjectsWithTag("Hero"))
        //{
        //    if(go != gameObject)
        //    {
        //        lastAlive = false;
        //    }
        //}

        //if(lastAlive)
        //{

        //}

        gameObject.SetActive(false);
    }

    [PunRPC]
    public void PlayerDied()
    {
        Debug.Log("PlayerDied");
    }
}
