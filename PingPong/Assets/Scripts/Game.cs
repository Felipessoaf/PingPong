using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject hero,monster;
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
		if(PhotonNetwork.room.PlayerCount>=2){
			PhotonNetwork.Instantiate(this.monster.name, monster.transform.position,monster.transform.rotation, 0);
		}
		else{
			PhotonNetwork.Instantiate(this.hero.name, hero.transform.position,hero.transform.rotation, 0);
		}
		

	}
	// Update is called once per frame
	void Update () {
		
	}
}
