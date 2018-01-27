using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEngine.Networking.NetworkSystem;
//using System.Runtime.InteropServices;

//public class MyMessage : MessageBase
//{
//    public NetworkInstanceId netId;
//}

public class Ping : Photon.PunBehaviour,IPunObservable
{

    [Tooltip("Distancia maxima do raio do pong inimigo")]
    public float PongRadius;
    public bool PortalActive;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;

    private bool canPing = false;

    private short MyMsgId = 1000;
    public bool pinging;
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        /*
        if (stream.isWriting)
        {
            if(pinging){
                stream.SendNext(pinging);
                pinging = false;
            }
        }
        else
        {
            // Network player, receive data
            if(stream.isReading && stream.Count >0){
                bool newping = (bool)stream.ReceiveNext();
                if(!pinging && newping){
                    //receiveping();
                    pinging = false;
                }
            }
        }
        */
    }


    private void Start()
    {
        //if (!isLocalPlayer) return;

        //DeployPing();
        StartCoroutine(ResetPingCooldown());
        //SetupClient();
    }
    private void Update()
    {
        //if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space) && canPing)
        {
            DeployPing();
            //pinging = true;
            StartCoroutine(ResetPingCooldown());
            
        }
        //receive pinging
        //if(pinging && !photonView.isMine){ //e o id do envio ! do meu
        //    //receiveping();
        //}
    }

    public void DeployPing()
    {
        this.photonView.RPC("receiveping", PhotonTargets.Others);
        
        GetPong();
    }

    void GetPong()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, PongRadius);
        float minDist = float.MaxValue;
        GameObject nearMonter = null;
        
        foreach(Collider c in colliders)
        {
            if(c.gameObject.CompareTag("Monster"))
            {
                if(Vector3.Distance(transform.position, c.transform.position) < minDist)
                {
                    minDist = Vector3.Distance(transform.position, c.transform.position);
                    nearMonter = c.gameObject;
                }
            }
        }

        if(nearMonter)
        {
            Debug.DrawLine(transform.position, nearMonter.transform.position);
        }

        if (PortalActive)
        {
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Portal"))
                {
                    Debug.DrawLine(transform.position, c.gameObject.transform.position);
                    break;
                }
            }
        }
    }

    IEnumerator ResetPingCooldown()
    {
        canPing = false;
        yield return new WaitForSeconds(PingCooldown);
        canPing = true;
    }

    IEnumerator PingSpawn()
    {
        while(PortalActive)
        {
            DeployPing();
            yield return new WaitForSeconds(PingRate);
        }
    }
}
