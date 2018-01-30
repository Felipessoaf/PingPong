using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision(8,8);
		StartCoroutine(waitfull());
	}

	public void Show(){
		GetComponent<Renderer>().enabled = true;
	}
	public void Hide(){
		GetComponent<Renderer>().enabled = false;
	}
	IEnumerator waitfull(){
		yield return new WaitUntil(()=>PhotonNetwork.playerList.Length==4);
		while(true){
			if(photonView.isMine && !GetComponent<Player>().joined){
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("Hero")){
					if(o!= this.gameObject){
						o.GetComponent<Hero>().Hide();
					}
				}
			}
			yield return new WaitForSecondsRealtime(1f);

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
