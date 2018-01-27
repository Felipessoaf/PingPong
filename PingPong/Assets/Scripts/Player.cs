using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	int id;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) return;
	}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

}
