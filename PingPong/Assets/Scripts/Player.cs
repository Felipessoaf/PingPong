using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	int id;
	public Camera mainCamera;
	void Start () 
	{
		if(isLocalPlayer)
			//mainCamera.GetComponent<CameraFollow>().target = this.gameObject.transform;
			Camera.main.GetComponent<CameraFollow>().target = this.gameObject.transform;
	}	
	void Update () {
		if (!isLocalPlayer) return;
	}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

}
