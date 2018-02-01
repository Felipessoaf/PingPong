using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : MonoBehaviour {

	[Header("This should be the Local Player")]
	public Transform target;
	public float smoothSpeed = 0.125f;

	public Vector3 offset;

	void Awake()
	{

	}	
	void FixedUpdate () 
	{
			
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
			if(target)	
			{	
				transform.position = smoothedPosition;
			}
	}
}
