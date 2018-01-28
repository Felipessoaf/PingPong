using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Portal : Photon.PunBehaviour
{
	public bool active = true;
	public int playerWin;
	public Text YouWinText;
	public ParticleSystem portalParticles;

	void Start()
	{
		//StartCoroutine(activatePortalTime(5f));
	}
		
	void OnCollisionEnter(Collision col)
	{
		if(col.collider.gameObject.tag == "Player" && active)
		{
			playerWin++;
			col.collider.gameObject.SetActive(false);
		}
	}
	void Update()
	{
		if(playerWin == 2)
		{
			YouWinText.enabled = true;
		}
	}

	IEnumerator activatePortalTime(float time)
	{
		Debug.Log("Portal time activated");
		yield return new WaitForSeconds(time);
        ActivatePortal();
	}

    public void ActivatePortal()
	{
        active = true;
		if(portalParticles.isStopped)portalParticles.Play();
		GetComponent<Collider>().enabled = true;
	}
}
