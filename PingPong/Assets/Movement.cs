using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour 
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
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		playerVelocityVector.x = Input.GetAxis("Horizontal");
		playerVelocityVector.z = Input.GetAxis("Vertical");

		if(Input.anyKey)
		{
			playerRb.velocity = playerVelocity * playerVelocityVector; //new Vector3 (0,0,playerVelocity);
			//Debug.Log("W");
		}
	}
}
