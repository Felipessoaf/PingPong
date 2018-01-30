using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

	public Camera mainCamera;
    public GameObject Visual;
    public bool Alive;
    public bool Won;
	public bool joined;
    void Awake()
    {
		if ( photonView.isMine)
		{
			CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
			FOV mainCameraFOV = Camera.main.GetComponent<FOV>();
            
			mainCameraFollow.target = this.gameObject.transform;
			mainCameraFOV.localPlayerTag = this.gameObject.tag;
			joined = false;
		}

    }

	[PunRPC]
    void Join()
    {

            joined = true;
			if(transform.tag=="Hero"){
				GetComponent<Hero>().Show();
			}
            GetComponent<Ping>().PortalActive = true;
            StartCoroutine(GetComponent<Ping>().PingSpawn());
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("Monster")){
            	PhotonView v = PhotonView.Get(o);
            	v.RPC("end", PhotonTargets.All);
        	}
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("Hero")){
            	PhotonView v = PhotonView.Get(o);
            	v.RPC("end", PhotonTargets.All);
        	}

    }
	[PunRPC]
    void end(PhotonMessageInfo info)
    {
		if(PhotonNetwork.inRoom)PhotonNetwork.LeaveRoom();
	}


}
