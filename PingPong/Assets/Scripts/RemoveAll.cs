using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAll : Photon.MonoBehaviour {
    

    public void EndAnim()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Monster"))
        {
            PhotonView v = PhotonView.Get(o);
            v.RPC("end", PhotonTargets.All);
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Hero"))
        {
            PhotonView v = PhotonView.Get(o);
            v.RPC("end", PhotonTargets.All);
        }
    }
}
