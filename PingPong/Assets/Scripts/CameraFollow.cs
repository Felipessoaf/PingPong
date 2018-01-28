using UnityEngine;
using UnityEngine.Networking;

	[RequireComponent(typeof(FOV))]
	public class CameraFollow : MonoBehaviour {

	[Header("This should be the Local Player")]
	public Transform target;
	[Space]
	public GameObject head;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void Awake()
	{

	}	
	void FixedUpdate () 
	{
		RaycastHit hitInfo;
		bool hit = Physics.Raycast(transform.position,transform.position-target.position,out hitInfo,Mathf.Infinity);
		if(hit)
		{
			//Debug.Log(hitInfo.collider.gameObject.name);
			//if(hitInfo.collider.transform.GetChild(1).GetComponent<MeshRenderer>())
			if(hitInfo.collider.GetComponent<MeshRenderer>())
			{
				//head.GetComponent<Outline>();
				//GetComponent<Outline>();
			}
		}

		if(target)	
		{
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
			transform.position = smoothedPosition;
			transform.rotation = Quaternion.Euler(30,0,0);
			//transform.LookAt(target);
		}
	}
	}