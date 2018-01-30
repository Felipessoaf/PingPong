using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Photon.MonoBehaviour {
    
    public Camera mainCamera;

    void Awake()
    {
        if (photonView.isMine)
        {
            CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
            FOV mainCameraFOV = Camera.main.GetComponent<FOV>();
            
            mainCameraFollow.target = this.gameObject.transform;
            mainCameraFOV.localPlayerTag = this.gameObject.tag;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
