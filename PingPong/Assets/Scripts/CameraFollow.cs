using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour {

	[Header("This should be the Local Player")]
	public Transform target;
	public float smoothSpeed = 0.125f;

	public Vector3 offset;

	void Awake()
	{

	}	
	void LateUpdate () 
	{
		if(target)
		transform.position = target.position + offset;
	}
}
