using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public static Game instance;
	// Use this for initialization
	void Start () {
		
		if(instance){
			 Destroy(this);
		}
		else{
			instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
