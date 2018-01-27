using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	int id;
	public Camera mainCamera;
	void Start () 
	{
		if (!isLocalPlayer)
		{
             GetComponent<MeshRenderer>().enabled = false;
             GetComponent<Collider>().enabled = false;
		}
	}	
	void Update () {
		if (!isLocalPlayer) return;
	}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
		Camera.main.GetComponent<CameraFollow>().target = this.gameObject.transform;

    }

}
