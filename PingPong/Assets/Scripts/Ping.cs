using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.Networking.NetworkSystem;
//using System.Runtime.InteropServices;

//public class MyMessage : MessageBase
//{
//    public NetworkInstanceId netId;
//}


public class Ping : Photon.MonoBehaviour
{

    [Tooltip("Distancia maxima do raio do pong inimigo")]
    public float UnionRadius;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;
    public float PingDuration = 0.5f;
	//public AudioSource PingSound;
	//public GameObject H1Line;


    //private Vector3 _startPos;
    //private Vector3 _endPos;

    public float PingWaitTime = 2f;
    public bool canPing = false;
    //List<GameObject> monsters;
    GameObject otherPlayer;

	private GameObject m1;
	private GameObject m2;

	public bool Playable = false;

    private void Start()
    {
        
        //monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        StartCoroutine(ResetPingCooldown());
        
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(canPing);
        }
        else
        {
            // Network player, receive data
            this.canPing = (bool)stream.ReceiveNext();
            //Debug.Log(canPing);
        }
    }
    private void Update()
    {
		
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

		if (Input.GetKeyDown(KeyCode.Space) && canPing && Playable)
        {
			Debug.Log ("Trying to ping here");
            newDeployPing();
            StartCoroutine(ResetPingCooldown());
        }
    }

	public void newDeployPing()
	{
        Collider[] colliders = Physics.OverlapSphere(transform.position, UnionRadius);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Hero") && !c.GetComponent<Ping>().canPing)
            {
                //GetComponent<Player>().JoinLocal();
                this.photonView.RPC("Join", PhotonTargets.All);

                foreach (GameObject o in GameObject.FindGameObjectsWithTag("Hero")) 
                {
                    if(o!= this.gameObject)
                    {
                        otherPlayer = o;

                        PhotonView.Get(otherPlayer).RPC("Join", PhotonTargets.All);
                    }
                }
                //Game.instance.SelectPortal();
                //PhotonView v = PhotonView.Get(otherPlayer);
                //v.RPC("Join", PhotonTargets.All);
                //photonView.RPC("Join", PhotonTargets.All);

            }
        }

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Hero")) 
		{
			if(o!= this.gameObject)
			{
				otherPlayer = o;
				//Debug.Log ("Player found" + "Position: " + otherPlayer.transform.position);
                PhotonView v = PhotonView.Get(otherPlayer);
                v.RPC("receiveping", PhotonTargets.Others,this.transform.position);
				//otherPlayer.GetComponent<Ping>().newReceivePing(this.transform.position);
			}
			if (!otherPlayer) 
			{
				Debug.Log ("Other player not found");
			}
		}
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Monster")) 
		{
			PhotonView v = PhotonView.Get(o);
            v.RPC("receiveping", PhotonTargets.Others,this.transform.position);
		}
		GetComponent<Pong>().GetPong ();
	}
    /*
    [PunRPC]
	public void newReceivePing(Vector3 otherPlayerPos)
	{
		Debug.Log ("Ping received and the Pos is: " + otherPlayerPos);
		myLine = GameObject.FindGameObjectWithTag("Line");



		//Setting up the line
		LineRenderer lineRender = myLine.GetComponent<LineRenderer> ();
		lineRender.enabled = true;

		//Setting the positions
		lineRender.SetPosition (0,  otherPlayerPos  + new Vector3 (0,1,0));
		lineRender.SetPosition (1,this.transform.position);

		StartCoroutine (DeleteRay());
	}
    */
    /*
	public void newGetPong()
	{
		Debug.Log ("Pong");
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Monster")) 
		{
            if (m1 == null){
                m1 = o;
            }
            else{
                m2 = o;
            }
		}
		if (Vector3.Distance (m1.transform.position, this.transform.position) < Vector3.Distance (m2.transform.position, this.transform.position)) 
		{
			H1Line.GetComponent<LineRenderer> ().SetPosition (0, m1.transform.position + Vector3.up);
			H1Line.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + Vector3.up);
		} 
		else 
		{
			H1Line.GetComponent<LineRenderer> ().SetPosition (0, m2.transform.position + Vector3.up);
			H1Line.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + Vector3.up);
		}
		
	}
    */
	/*
    public void DeployPing()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Player")){
            if(o!= this.gameObject){
                otherPlayer = o;
            }
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, UnionRadius);
        foreach(Collider c in colliders)
        {
            if(c.gameObject.CompareTag("Player") && !c.GetComponent<Ping>().canPing)
            {
                Game.instance.SelectPortal();
                PhotonView v = PhotonView.Get(otherPlayer);
                v.RPC("Join", PhotonTargets.All);
                photonView.RPC("Join", PhotonTargets.All);
                
            }
        }
        
        PhotonView view = PhotonView.Get(otherPlayer);
        view.RPC("receiveping", PhotonTargets.All,transform.position);
        

        foreach(GameObject o in monsters){
            PhotonView v = PhotonView.Get(o);
            v.RPC("receiveping", PhotonTargets.All,transform.position);
        }

		if (PingSound) {
			PingSound.Play ();
		}

        newGetPong();
    }*/
    
    
    
    IEnumerator ResetPingCooldown()
    {
        canPing = false;
        yield return new WaitForSeconds(PingCooldown);
        canPing = true;
    }

    public IEnumerator PingSpawn()
    {
        while(GetComponent<Hero>().joined)
        {
            //DeployPing();
            yield return new WaitForSeconds(PingRate);
        }
    }

   
}
