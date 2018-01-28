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

public class Ping : Photon.PunBehaviour
{

    [Tooltip("Distancia maxima do raio do pong inimigo")]
    public float PongRadius,UnionRadius;
    public bool PortalActive;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;

    public float PingWaitTime = 2f;
    public bool canPing = false;
    List<GameObject> monsters;
    GameObject other;
    private void Start()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Player")){
            if(o!= this.gameObject){
                other = o;
            }
        }
        monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        StartCoroutine(ResetPingCooldown());
        
    }
    private void Update()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canPing)
        {
            DeployPing();
            StartCoroutine(ResetPingCooldown());
        }
    }

    public void DeployPing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, UnionRadius);
        float minDist = float.MaxValue;
        GameObject nearMonter = null;
        
        foreach(Collider c in colliders)
        {
            if(c.gameObject.CompareTag("Player") && !c.GetComponent<Ping>().canPing)
            {
                
                    PhotonView v = PhotonView.Get(other);
                    v.RPC("Join", PhotonTargets.All,transform.position);
                    photonView.RPC("Join", PhotonTargets.All,transform.position);
                
            }
        }
        
        PhotonView view = PhotonView.Get(other);
        view.RPC("receiveping", PhotonTargets.All,transform.position);
        

        foreach(GameObject o in monsters){
            PhotonView v = PhotonView.Get(o);
            v.RPC("receiveping", PhotonTargets.All,transform.position);
        }
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
