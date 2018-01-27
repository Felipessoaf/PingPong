using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
		if(instance){
			 Destroy(this);
		}
		else{
			instance = this;
		}
		begin();
	}
	public void begin(){

		PhotonNetwork.Instantiate(this.player.name, player.transform.position,player.transform.rotation, 0);
		

	}
	// Update is called once per frame
	void Update () {
		
	}
}
