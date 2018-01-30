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
    public float PongRadius,UnionRadius;
    public bool PortalActive;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;
    //public Material RayMat;
    public float PingDuration = 0.5f;
	public AudioSource PingSound;

    private GameObject myLine;
    //private Vector3 _startPos;
    //private Vector3 _endPos;

    public float PingWaitTime = 2f;
    public bool canPing = false;
    //List<GameObject> monsters;
    GameObject otherPlayer;
	GameObject Monster1;

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
                v.RPC("newReceivePing", PhotonTargets.Others,this.transform.position);
				//otherPlayer.GetComponent<Ping>().newReceivePing(this.transform.position);
			}
			if (!otherPlayer) 
			{
				Debug.Log ("Other player not found");
			}
		}
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Monster")) 
		{
			if(o!= this.gameObject)
			{
				Monster1 = o;
				Debug.Log ("Monster found" + "Position: " + otherPlayer.transform.position);
				Debug.Log("Monster Distance: " + Vector3.Distance(this.transform.position,otherPlayer.transform.position));
			}
			if (!Monster1) 
			{
				Debug.Log ("Monster1 not found");
			}
		}
	}
    [PunRPC]
	public void newReceivePing(Vector3 otherPlayerPos)
	{
		Debug.Log ("Ping received and the Pos is: " + otherPlayerPos);
		myLine = GameObject.FindGameObjectWithTag("Line");



		//Setting up the line
		LineRenderer lineRender = myLine.GetComponent<LineRenderer> ();
		lineRender.enabled = true;

		//Setting the positions
		lineRender.SetPosition (0, this.transform.position);
		lineRender.SetPosition (1, otherPlayerPos  + new Vector3 (0,1,0) );

		StartCoroutine (DeleteRay());
	}

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

        GetPong();
    }*/
    /*
    void GetPong()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, PongRadius);
        float minDist = float.MaxValue;
        GameObject nearMonter = null;
        
        foreach(Collider c in colliders)
        {
            if(c.gameObject.CompareTag("Monster"))
            {
                if(Vector3.Distance(transform.position, c.transform.position) < minDist)
                {
                    minDist = Vector3.Distance(transform.position, c.transform.position);
                    nearMonter = c.gameObject;
                }
            }
        }

        if(nearMonter)
        {
            _startPos = transform.position;
            _endPos = nearMonter.transform.position;

            if (myLine)
            {
                Destroy(myLine);
            }

            myLine = new GameObject();
            myLine.transform.position = _startPos;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = RayMat;
            lr.startColor = Color.white;
            lr.endColor = Color.red;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, _startPos);
            lr.SetPosition(1, _endPos);

            StartCoroutine(DeleteRay());
        }

        if (PortalActive)
        {
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Portal") && c.gameObject.GetComponent<Portal>().active)
                {
                    Debug.DrawLine(transform.position, c.gameObject.transform.position);
                    break;
                }
            }
        }
    }
    */
    IEnumerator ResetPingCooldown()
    {
        canPing = false;
        yield return new WaitForSeconds(PingCooldown);
        canPing = true;
    }

    public IEnumerator PingSpawn()
    {
        while(PortalActive)
        {
            //DeployPing();
            yield return new WaitForSeconds(PingRate);
        }
    }

    IEnumerator DeleteRay()
    {
        yield return new WaitForSeconds(PingDuration);
        if (myLine)
        {
            //Destroy(myLine);
			myLine.GetComponent<LineRenderer>().enabled = false;
        }
    }
}
