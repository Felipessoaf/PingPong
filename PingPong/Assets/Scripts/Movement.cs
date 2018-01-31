using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Movement : Photon.MonoBehaviour 
{
	[Range(1.0f,50f)]
	public float playerVelocity = 10f;
    public Vector3 playerVelocityVector;

    public float Speed;

    public GameObject Frente;
    public GameObject Tras;
    public Animator Anim1;
    public Animator Anim2;

    private Rigidbody rb;
    private Vector3 vel;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        vel = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            vel += Vector3.left * Speed;

            if (Anim1)
            {
                Anim1.SetBool("left", true);
            }
            if (Anim2)
            {
                Anim2.SetBool("left", true);
            }

            if (Anim1)
            {
                Anim1.SetBool("lado", true);
            }
            if (Anim2)
            {
                Anim2.SetBool("lado", true);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            vel += Vector3.right * Speed;
            
            if (Anim1)
            {
                Anim1.SetBool("left", false);
            }
            if (Anim2)
            {
                Anim2.SetBool("left", false);
            }

            if (Anim1)
            {
                Anim1.SetBool("lado", true);
            }
            if (Anim2)
            {
                Anim2.SetBool("lado", true);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            vel += Vector3.forward * Speed * 1.5f;

            if (Anim1)
            {
                Anim1.SetBool("lado", false);
            }
            if (Anim2)
            {
                Anim2.SetBool("lado", false);
            }

            if (Frente && Tras)
            {
                Frente.SetActive(false);
                Tras.SetActive(true);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            vel += Vector3.back * Speed * 1.5f;

            if (Anim1)
            {
                Anim1.SetBool("lado", false);
            }
            if (Anim2)
            {
                Anim2.SetBool("lado", false);
            }

            if (Frente && Tras)
            {
                Frente.SetActive(true);
                Tras.SetActive(false);
            }
        }

        rb.velocity = vel;
        if(rb.velocity.x == 0 && rb.velocity.z == 0)
        {
            if (Anim1)
            {
                Anim1.SetBool("moving", false);
            }
            if (Anim2)
            {
                Anim2.SetBool("moving", false);
            }
        }
        else
        {
            if (Anim1)
            {
                Anim1.SetBool("moving", true);
            }
            if (Anim2)
            {
                Anim2.SetBool("moving", true);
            }
        }
        /*
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
		    playerVelocityVector.x = Input.GetAxis("Horizontal");
		    playerVelocityVector.z = Input.GetAxis("Vertical");

            if (playerVelocityVector.z <= 0)
            {
                if(Frente && Tras)
                {
                    Frente.SetActive(true);
                    Tras.SetActive(false);
                }
            }
            else
            {
                if (Frente && Tras)
                {
                    Frente.SetActive(false);
                    Tras.SetActive(true);
                }
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
        }*/
    }
	void FixedUpdate () 
	{		
		//rb.velocity = playerVelocity * playerVelocityVector.normalized;
  //      rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * 1.5f);
	}
}
