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
			if(target)	
			{
				Vector3 desiredPosition = target.position + offset;
				Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
				transform.position = smoothedPosition;
				//transform.LookAt(target);
			}
	}
}
