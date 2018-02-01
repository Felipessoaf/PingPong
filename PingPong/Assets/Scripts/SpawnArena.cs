using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArena : MonoBehaviour {

	public GameObject Tile1;
	private int Columns;
	private int Rows = -1;
	private float tileSize;

	
	void Start () 
	{
		tileSize = Tile1.transform.localScale.x;

		for(int i=0;i<9;i++)
		{
			Columns++;
			if(i%3 == 0)
			{
				Columns = 0;
				Rows++;
			}			
			Instantiate(Tile1,new Vector3 (10*tileSize*Columns,-0.5f,10*tileSize*Rows),Quaternion.identity,this.gameObject.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
