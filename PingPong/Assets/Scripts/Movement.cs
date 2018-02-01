using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Movement : Photon.MonoBehaviour 
{
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
                VisualActive(Frente, false);
                VisualActive(Tras, true);
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
                VisualActive(Frente, true);
                VisualActive(Tras, false);
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
    }

    public void VisualActive(GameObject go, bool active)
    {
        foreach(Transform c in go.transform)
        {
            SkinnedMeshRenderer sm = c.GetComponent<SkinnedMeshRenderer>();
            if(sm)
            {
                sm.enabled = active;
            }
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr)
            {
                sr.enabled = active;
            }
            MeshRenderer mr = c.GetComponent<MeshRenderer>();
            if (mr)
            {
                mr.enabled = active;
            }
        }
    }
}
