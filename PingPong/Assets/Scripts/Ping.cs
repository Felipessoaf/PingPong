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
    public float PongRadius;
    public bool PortalActive;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;
    public Material RayMat;
    public float PingDuration = 0.5f;

    private GameObject myLine;
    private Vector3 _startPos;
    private Vector3 _endPos;

    private bool canPing = false;
    
    private void Start()
    {
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
        //this.photonView.RPC("receiveping", PhotonTargets.Others);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject o in players){
            if(o!= this.gameObject){
                PhotonView view = PhotonView.Get(o);
                view.RPC("receiveping", PhotonTargets.All,transform.position);
            }
        }
        foreach(GameObject o in enemies){
            PhotonView view = PhotonView.Get(o);
            view.RPC("receiveping", PhotonTargets.All,transform.position);
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
            _startPos = transform.position;
            _endPos = nearMonter.transform.position;

            if (myLine)
            {
                Destroy(myLine);
            }

            myLine = new GameObject();
            myLine.transform.position = _startPos;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = RayMat;
            lr.startColor = Color.white;
            lr.endColor = Color.black;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, _startPos);
            lr.SetPosition(1, _endPos);

            StartCoroutine(DeleteRay());
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

    IEnumerator DeleteRay()
    {
        yield return new WaitForSeconds(PingDuration);
        if (myLine)
        {
            Destroy(myLine);
        }
    }
}
