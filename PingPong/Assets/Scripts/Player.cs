using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {

	public Camera mainCamera;
	public static GameObject LocalPlayerInstance;
	void Awake(){
		if ( photonView.isMine)
		{
			Player.LocalPlayerInstance = this.gameObject;
			CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
			FOV mainCameraFOV = Camera.main.GetComponent<FOV>();
            
			mainCameraFollow.target = this.gameObject.transform;
			mainCameraFOV.localPlayerTag = this.gameObject.tag;
		}
		
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () 
	{
		//if (!isLocalPlayer)
		{
             //GetComponent<MeshRenderer>().enabled = false;
             //GetComponent<Collider>().enabled = false;
		}
	}	
	void Update () {
		//if (!isLocalPlayer) return;
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
	}
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
	[PunRPC]
    public void Join()
    {
        if (!photonView.isMine)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

}
