using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ping : MonoBehaviour
{

    [Tooltip("Distancia maxima do raio do pong inimigo")]
    public float PongRadius;
    public bool PortalActive;
    public Material RayMat;
    public float PingDuration = 0.5f;
    public float PingCooldown = 1.5f;

    private bool canPing = false;

    private GameObject myLine;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector2 _diff;
    private float _dist;

    private void Start()
    {
        DeployPing();
        StartCoroutine(ResetPingCooldown());
    }

    private void Update()
    {
        //if (!isLocalPlayer) return;

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

        if(otherPlayer)
        {
            otherPlayer.GetComponent<Ping>().GetPing(transform);
        }

        GetPong();
    }

    public void GetPing(Transform t)
    {
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
}
