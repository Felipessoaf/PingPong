using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

	int id;
	public Camera mainCamera;
	public static GameObject LocalPlayerInstance;
	void Awake(){
		if ( photonView.isMine)
		{
			Player.LocalPlayerInstance = this.gameObject;
			CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
			FOV mainCameraFOV = Camera.main.GetComponent<FOV>();

			GetComponent<MeshRenderer>().material.color = Color.red;
			mainCameraFollow.target = this.gameObject.transform;
			mainCameraFOV.localPlayerTag = this.gameObject.tag;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () 
	{
		//if (!isLocalPlayer)
		{
             GetComponent<MeshRenderer>().enabled = false;
             GetComponent<Collider>().enabled = false;
		}
	}	
	void Update () {
		//if (!isLocalPlayer) return;
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
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
