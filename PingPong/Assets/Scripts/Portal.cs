using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour 
{
		public bool activate = true;
		public int playerWin;
		public ParticleSystem portalParticles;

		void Start()
		{
			StartCoroutine(activatePortalTime(5f));
		}
		
		void OnCollisionEnter(Collision col)
		{
			if(col.collider.gameObject.tag == "Player" && activate)
			{
				playerWin++;
				col.collider.gameObject.SetActive(false);
			}
		}

	IEnumerator activatePortalTime(float time)
	{
		Debug.Log("Portal time activated");
		yield return new WaitForSeconds(time);
		activatePortal();
	}
	void activatePortal()
	{
		activate = true;
		if(portalParticles.isStopped)portalParticles.Play();
		GetComponent<Collider>().enabled = true;
	}
}
