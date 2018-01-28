using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : Photon.PunBehaviour {
	//public int id;
	public Text num,title;
	public Toggle toggle;
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
	void Start () {

		//if(id!=PhotonNetwork.player.ID) toggle.interactable = false;
		title.text = PhotonNetwork.room.Name;
	}
	
	void Update()
	{
		num.text = PhotonNetwork.room.PlayerCount.ToString();
	}
	public void Ready(){

		if(PhotonNetwork.room.PlayerCount==4){
			//se todo mundo estiver ready
			PhotonNetwork.LoadLevel("lobby");

		}
	}
	
	
}
