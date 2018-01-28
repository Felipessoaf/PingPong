using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Movement : Photon.MonoBehaviour 
{
	[Range(1.0f,50f)]
	public float playerVelocity;
    public Vector3 playerVelocityVector;

    public GameObject Frente;
    public GameObject Tras;
    public Animator Anim1;
    public Animator Anim2;

    Rigidbody playerRb;

	void Awake()
	{
		playerRb = GetComponent<Rigidbody>();
	}
    void Start () {

    }
	
	void Update()
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
		if(Input.anyKey)
		{
		    playerVelocityVector.x = Input.GetAxis("Horizontal");
		    playerVelocityVector.z = Input.GetAxis("Vertical");

            if (playerVelocityVector.z <= 0)
            {
                Frente.SetActive(true);
                Tras.SetActive(false);
            }
            else
            {
                Frente.SetActive(false);
                Tras.SetActive(true);
            }

            if (Anim1 && Anim2)
            {
                Anim1.SetBool("moving", true);
                Anim2.SetBool("moving", true);
                if(playerVelocityVector.x < 0)
                {
                    Anim1.SetBool("left", true);
                    Anim2.SetBool("left", true);
                }
                else
                {
                    Anim1.SetBool("left", false);
                    Anim2.SetBool("left", false);
                }
                if (playerVelocityVector.z == 0)
                {
                    Anim1.SetBool("lado", true);
                    Anim2.SetBool("lado", true);
                }
                else
                {
                    Anim1.SetBool("lado", false);
                    Anim2.SetBool("lado", false);
                }
            }
        }
		else
		{
			playerVelocityVector = Vector3.zero;
            if (Anim1 && Anim2)
            {
                Anim1.SetBool("moving", false);
                Anim2.SetBool("moving", false);
            }
        }
	}
	void FixedUpdate () 
	{		
		playerRb.velocity = playerVelocity * playerVelocityVector.normalized;
        playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, playerRb.velocity.z * 1.5f);
	}
}
