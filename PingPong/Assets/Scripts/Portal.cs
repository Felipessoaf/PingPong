using UnityEngine;

public class Portal : MonoBehaviour 
{
	public bool activate = true;
	public int playerWin;
	
	  void OnCollisionEnter(Collision col)
	{
		if(col.collider.gameObject.tag == "Player" && activate)
		{
			playerWin++;
			col.collider.gameObject.SetActive(false);
		}
	}
}
