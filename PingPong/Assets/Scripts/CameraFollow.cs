using UnityEngine;
using UnityEngine.Networking;
using cakeslice;

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
		//GetComponent<Outline>();
	}	
	void FixedUpdate () 
	{
		RaycastHit hitInfo;
		bool hit = Physics.Raycast(transform.position,target.position - transform.position,out hitInfo,Mathf.Infinity);
		Debug.DrawRay(transform.position,target.position - transform.position,Color.green,0.5f);
		if(hit)
		{
			//Debug.Log(hitInfo.collider.gameObject.name);
			//if(hitInfo.collider.transform.GetChild(1).GetComponent<MeshRenderer>())
			if(hitInfo.collider.GetComponent<Collider>() && hitInfo.collider.tag != "Player" || hitInfo.collider.tag != "Monster")
			{
				Debug.Log(hitInfo.collider.name);
				GetComponent<OutlineEffect>().lineThickness = 1.25f;
				GetComponent<OutlineEffect>().lineIntensity = 0.6f;
			}	
			else
			{
				Debug.Log("Effect out");
				GetComponent<OutlineEffect>().lineIntensity = 0f;
				GetComponent<OutlineEffect>().lineThickness = 0f;
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