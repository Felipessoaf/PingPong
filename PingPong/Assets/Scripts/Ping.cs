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

public class Ping : NetworkBehaviour
{

    [Tooltip("Distancia maxima do raio do pong inimigo")]
    public float PongRadius;
    public bool PortalActive;
    public Material RayMat;
    public float PingDuration = 0.5f;
    public float PingCooldown = 1.5f;
    public float PingRate = 1f;

    private bool canPing = false;

    private GameObject myLine;
    private Vector3 _startPos;
    private Vector3 _endPos;

    private short MyMsgId = 1000;
    
    //NetworkClient myClient;

    //public class MyMsgType
    //{
    //    public static short Score = MsgType.Highest + 1;
    //};

    //public class ScoreMessage : MessageBase
    //{
    //    public NetworkInstanceId netId;
    //}

    //[Command]
    //public void CmdSendScore(NetworkInstanceId id)
    //{
    //    ScoreMessage msg = new ScoreMessage();
    //    msg.netId = id;

    //    NetworkServer.SendToAll(MyMsgType.Score, msg);
    //}

    //// Create a client and connect to the server port
    //public void SetupClient()
    //{
    //    myClient = new NetworkClient();
    //    myClient.RegisterHandler(MsgType.Connect, OnConnected);
    //    myClient.RegisterHandler(MyMsgType.Score, OnScore);
    //    myClient.Connect("127.0.0.1", 7777);
    //}

    //public void OnScore(NetworkMessage netMsg)
    //{
    //    ScoreMessage msg = netMsg.ReadMessage<ScoreMessage>();
    //    Debug.Log("OnScoreMessage " + msg.netId);
    //    var player = ClientScene.FindLocalObject(msg.netId);
    //    player.GetComponent<Ping>().GetPing(player.transform);
    //}

    //public void OnConnected(NetworkMessage netMsg)
    //{
    //    Debug.Log("Connected to server");
    //}

    //public override void OnStartClient()
    //{
    //    // this should be somewhere else..
    //    NetworkManager.singleton.client.RegisterHandler(MyMsgId, OnMyMsg);
    //}

    //[Command]
    //void CmdSendToMe()
    //{
    //    var msg = new MyMessage();
    //    msg.netId = netId;

    //    //base.connectionToClient.Send(MyMsgId, msg);
    //    NetworkServer.SendToAll(MyMsgId, msg);
    //}

    //static void OnMyMsg(NetworkMessage netMsg)
    //{
    //    var msg = netMsg.ReadMessage<MyMessage>();
    //    var player = ClientScene.FindLocalObject(msg.netId);
    //    player.GetComponent<Ping>().GetPing(player.transform);
    //}

    private void Start()
    {
        if (!isLocalPlayer) return;

        DeployPing();
        StartCoroutine(ResetPingCooldown());
        //SetupClient();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space) && canPing)
        {
            DeployPing();
            StartCoroutine(ResetPingCooldown());
        }
    }

    public void DeployPing()
    {
        GameObject otherPlayer = null;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(go != gameObject)
            {
                otherPlayer = go;
            }
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Monster"))
        {
            //send msg
        }

        if (otherPlayer)
        {
            //send msg 
            otherPlayer.GetComponent<Ping>().GetPing(transform);
            //CmdSendToMe();
            //var msg = new MyMessage();
            //msg.netId = netId;

            //base.connectionToClient.Send(MyMsgId, msg);
            //NetworkServer.SendToAll(MyMsgId, msg);
            //CmdSendScore(netId);
        }

        GetPong();
    }

    public void GetPing(Transform t)
    {
        if (t.gameObject == gameObject)
        {
            Debug.Log("Mesmo player");
            //return;
        }

        _startPos = transform.position;
        _endPos = t.position;

        if (myLine)
        {
            Destroy(myLine);
        }

        myLine = new GameObject();
        myLine.transform.position = _startPos;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = RayMat;
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, _startPos);
        lr.SetPosition(1, _endPos);

        Debug.DrawLine(transform.position, t.position);
        Debug.Log("Transform: " + t.position);

        StartCoroutine(DeleteRay());
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

    IEnumerator DeleteRay()
    {
        yield return new WaitForSeconds(PingDuration);
        if (myLine)
        {
            Destroy(myLine);
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
