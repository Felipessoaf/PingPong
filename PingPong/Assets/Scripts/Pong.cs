using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pong : MonoBehaviour {
	public float PongRadius,PongDuration;
	
	public Material RayMat;
	private GameObject myLine;

	public void GetPong()
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
				Vector3 _startPos = transform.position;
				Vector3 _endPos = nearMonter.transform.position;

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
				lr.endColor = Color.red;
				lr.startWidth = 0.1f;
				lr.endWidth = 0.1f;
				lr.SetPosition(0, _startPos);
				lr.SetPosition(1, _endPos);

				StartCoroutine(DeleteRay());
			}

			if (GetComponent<Hero>().joined)
			{
				foreach (Collider c in colliders)
				{
					if (c.gameObject.CompareTag("Portal") && c.gameObject.GetComponent<Portal>().active)
					{
						Debug.DrawLine(transform.position, c.gameObject.transform.position);
						break;
					}
				}
			}
		}
	IEnumerator DeleteRay()
    {
        yield return new WaitForSeconds(PongDuration);
        if (myLine)
        {
            //Destroy(myLine);
			myLine.GetComponent<LineRenderer>().enabled = false;
        }
    }
}
