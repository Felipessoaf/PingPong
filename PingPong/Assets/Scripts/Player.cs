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
		CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
		FOV mainCameraFOV = Camera.main.GetComponent<FOV>();

        GetComponent<MeshRenderer>().material.color = Color.red;
		mainCameraFollow.target = this.gameObject.transform;
		mainCameraFOV.localPlayerTag = this.gameObject.tag;
    }

    public void Join()
    {
        if (!isLocalPlayer)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

}
