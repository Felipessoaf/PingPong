using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PhotonManager : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
	public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
	void LoadArena(){
		if (!PhotonNetwork.isMasterClient) 
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}
		if(PhotonNetwork.room.PlayerCount==2){
			//PhotonNetwork.LoadLevel("main");
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer other){
		//Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting
	
	
		if (PhotonNetwork.isMasterClient) 
		{
			Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
	
			
			LoadArena();
		}
	}
	
	
	public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
	{
		//Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects
	
	
		if (PhotonNetwork.isMasterClient) 
		{
			Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
	
	
			//LoadArena();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
