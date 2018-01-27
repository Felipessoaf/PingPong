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
            if (go != gameObject)
            {
                otherPlayer = go;
            }
        }
        DrawPing(otherPlayer.transform);
    }

    public void DrawPing(Transform t)
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
        lr.endColor = Color.white;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, _startPos);
        lr.SetPosition(1, _endPos);

        Debug.DrawLine(transform.position, t.position);
        Debug.Log("Transform: " + t.position);

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
