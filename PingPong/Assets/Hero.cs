using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(waitfull());
	}
	IEnumerator waitfull(){
		yield return new WaitUntil(()=>PhotonNetwork.playerList.Length==2);
		while(true){
			if(photonView.isMine){
				foreach(GameObject o in GameObject.FindGameObjectsWithTag("Hero")){
					if(o!= this.gameObject){
						o.GetComponent<Renderer>().enabled = false;
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
