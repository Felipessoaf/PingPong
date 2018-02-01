using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingReceive : Photon.PunBehaviour
{
    public Material RayMat;
    public float PingDuration = 0.5f;
    public GameObject particle;
    private GameObject myLine;
    private Vector3 _startPos;
    private Vector3 _endPos;

    [PunRPC]
    void receiveping(Vector3 pos,PhotonMessageInfo info)
    {
        
        /*GameObject otherPlayer = null;
        
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(info.photonView.viewID == id)
            //if (go.GetComponent<Player>().id == id) 
            {
                otherPlayer = go;
            }
        }
        */
        
        
        if ( photonView.isMine)
        {
            DrawPing(pos);
        }
    }

    public void DrawPing(Vector3 t)
    {
        _startPos = transform.position;
        _endPos = t;
        //GameObject p = Instantiate(particle);
        //p.transform.position = _startPos;
        //p.transform.LookAt(_endPos);

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

    IEnumerator DeleteRay()
    {
        yield return new WaitForSeconds(PingDuration);
        if (myLine)
        {
            Destroy(myLine);
        }
    }
}
