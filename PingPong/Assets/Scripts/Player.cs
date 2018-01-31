using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

    public GameObject Visual;
    public bool Alive;
    public bool Won;
    void Awake()
    {
		if ( photonView.isMine)
		{
			CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
			FOV mainCameraFOV = Camera.main.GetComponent<FOV>();
            
			mainCameraFollow.target = this.gameObject.transform;
			mainCameraFOV.localPlayerTag = this.gameObject.tag;
		}

    }

	//recebido por todos os heros em todas instancias 
	[PunRPC]
    void Join()
    {

			if(transform.tag=="Hero"){
				GetComponent<Hero>().joined = true;
				GetComponent<Hero>().Show();
			}
            StartCoroutine(GetComponent<Ping>().PingSpawn());


    }
	[PunRPC]
    void end(PhotonMessageInfo info)
    {
		if(PhotonNetwork.isMasterClient){
			PhotonNetwork.LoadLevel("init");
		}
		
	}


}
