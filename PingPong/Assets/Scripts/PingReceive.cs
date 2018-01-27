using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingReceive : Photon.PunBehaviour
{
    public Material RayMat;
    public float PingDuration = 0.5f;

    private GameObject myLine;
    private Vector3 _startPos;
    private Vector3 _endPos;

    [PunRPC]
    void receiveping(PhotonMessageInfo info)
    {
        GameObject otherPlayer = null;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (go.GetPhotonView().viewID != info.photonView.viewID)
            {
                otherPlayer = go;
            }
        }

        if (otherPlayer && !photonView.isMine)
        {
            DrawPing(otherPlayer.transform.position);
        }
    }

    public void DrawPing(Vector3 t)
    {
        _startPos = transform.position;
        _endPos = t;

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

        //Debug.DrawLine(transform.position, t);
        Debug.Log("Transform other: " + t);
        Debug.Log("Transform mine: " + transform);

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
