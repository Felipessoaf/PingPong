using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

	public Camera mainCamera;
	public static GameObject LocalPlayerInstance;
    public GameObject Visual;

	void Awake()
    {
		if ( photonView.isMine)
		{
			Player.LocalPlayerInstance = this.gameObject;
			CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
			FOV mainCameraFOV = Camera.main.GetComponent<FOV>();
            
			mainCameraFollow.target = this.gameObject.transform;
			mainCameraFOV.localPlayerTag = this.gameObject.tag;
		}
        else if(PhotonNetwork.connected == true)
        {
            if(photonView.group==0)Visual.SetActive(false);
            //GetComponent<Collider>().enabled = false;
            return;
        }
		DontDestroyOnLoad(this.gameObject);
        Physics.IgnoreLayerCollision(8,8);

    }

	void Start () 
	{

	}	

	void Update ()
    {
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
	}

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
	[PunRPC]
    void Join(PhotonMessageInfo info)
    {
		if (!photonView.isMine)
        {
            Visual.SetActive(true);
            GetComponent<Ping>().PortalActive = true;
            StartCoroutine(GetComponent<Ping>().PingSpawn());
            //GetComponent<Collider>().enabled = true;
        }
    }

}
