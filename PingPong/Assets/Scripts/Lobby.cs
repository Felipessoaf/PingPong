using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : Photon.PunBehaviour {
	//public int id;
	public Text num,title,role;
	public Image[] imgs;
	//public Toggle toggle;
	/*
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(toggle.isOn);
        }
        else
        {
            // Network player, receive data
            toggle.isOn = (bool)stream.ReceiveNext();
            //Debug.Log(canPing);
        }
    }*/
	[PunRPC]
	void activate(int i){
		imgs[PhotonNetwork.room.PlayerCount-1].color = Color.white;
	}
	void Start () {

		//if(id!=PhotonNetwork.player.ID) toggle.interactable = false;
		title.text = PhotonNetwork.room.Name;
		//if(PhotonNetwork.room.PlayerCount>2){
		//PlayerPrefs.SetInt("character",PhotonNetwork.room.PlayerCount);
		if(PhotonNetwork.room.PlayerCount==1){
			PlayerPrefs.SetString("character","manhero");
		}
		if(PhotonNetwork.room.PlayerCount==2){
			PlayerPrefs.SetString("character","womanhero");
		}
		if(PhotonNetwork.room.PlayerCount==3){
			PlayerPrefs.SetString("character","monster1");	
		}
		if(PhotonNetwork.room.PlayerCount==4){
			PlayerPrefs.SetString("character","monster2");	
		}
		photonView.RPC("activate", PhotonTargets.Others,PhotonNetwork.room.PlayerCount-1);
		for(int i=0;i<PhotonNetwork.room.PlayerCount-1;i++){
			imgs[i].color = Color.white;
		}
		imgs[PhotonNetwork.room.PlayerCount-1].color = Color.yellow;

			//photonView.group = 1;
		//}
		//else{
		//	PlayerPrefs.SetString("character",false.ToString());
		//	photonView.group = 0;
		//}
	}
	
	void Update()
	{
		num.text = PhotonNetwork.room.PlayerCount.ToString()+"/4";
		
		if(PhotonNetwork.room.PlayerCount==4){
			//se todo mundo estiver ready
			PhotonNetwork.LoadLevel("NetworkMVP");

		}
	}
	public void Ready(){

		if(PhotonNetwork.room.PlayerCount==4){
			//se todo mundo estiver ready
			PhotonNetwork.LoadLevel("NetworkMVP");

		}
	}
	
	
}
