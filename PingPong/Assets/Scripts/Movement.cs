using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Movement : NetworkBehaviour 
{
	[Range(1.0f,50f)]
	public float playerVelocity;
    public Vector3 playerVelocityVector;

	Rigidbody playerRb;

	void Awake()
	{
		playerRb = GetComponent<Rigidbody>();
	}
    void Start () {
		
	}
	
	void Update()
	{
		if (!isLocalPlayer) return;
		if(Input.anyKey)
		{
		playerVelocityVector.x = Input.GetAxis("Horizontal");
		playerVelocityVector.z = Input.GetAxis("Vertical");
		}
		else
		{
			//playerVelocityVector = Vector3.zero;
		}
	}
	void FixedUpdate () 
	{		
			playerRb.velocity = playerVelocity * playerVelocityVector;
	}
}
