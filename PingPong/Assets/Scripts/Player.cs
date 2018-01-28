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
            Visual.SetActive(false);
            GetComponent<Collider>().enabled = false;
            return;
        }
		DontDestroyOnLoad(this.gameObject);
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

	[PunRPC]
    void Join(PhotonMessageInfo info)
    {
        Debug.Log("aaa");
		if (!photonView.isMine)
        {
            Visual.SetActive(false);
            GetComponent<Collider>().enabled = true;
        }
    }

}
