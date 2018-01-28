using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.PunBehaviour {

	public Camera mainCamera;
	public static GameObject LocalPlayerInstance;
    public GameObject Visual;
    public bool Alive;
    public bool Won;

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

		DontDestroyOnLoad(this.gameObject);
        Physics.IgnoreLayerCollision(8,8);

    }
	public void Show(){
		Visual.SetActive(true);
	}
	public void Hide(){
		Visual.SetActive(false);
	}
	IEnumerator disableplayers(){
		yield return new WaitForSeconds(1f);
		if ( photonView.isMine)
		{
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("Player")){
				if(o!= this.gameObject){
					o.GetComponent<Player>().Hide();
				}
			}
		}
	}
	void Start () 
	{
		StartCoroutine(disableplayers());
		
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
            
			Show();
            GetComponent<Ping>().PortalActive = true;
            StartCoroutine(GetComponent<Ping>().PingSpawn());
            //GetComponent<Collider>().enabled = true;
        }
    }

}
