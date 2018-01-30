using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject hero,monster;
    public Transform[] spawnpoints;
    //private List<GameObject> portals;
	void Start () {
		
		if(instance){
			 Destroy(this);
		}
		else{
			instance = this;
            Begin();
        }

	}
	public void Begin(){
		string character =  PlayerPrefs.GetString("character");
        Vector3 pos = spawnpoints[PhotonNetwork.playerList.Length-1].position;
        if(character=="monster"){
            //instancia em todo mundo
			PhotonNetwork.Instantiate(this.monster.name, pos,monster.transform.rotation, 0);
		}
        if(character=="hero"){
            //instancia em todo mundo
			PhotonNetwork.Instantiate(this.hero.name, pos,hero.transform.rotation, 0);            
			//if(ch==1) PhotonNetwork.Instantiate(this.woman.name, woman.transform.position,woman.transform.rotation, 0);
			//if(ch==2) PhotonNetwork.Instantiate(this.man.name, man.transform.position,man.transform.rotation, 0);
		}

        //portals = new List<GameObject>();
        //foreach (GameObject o in GameObject.FindGameObjectsWithTag("Portal"))
        //{
        //    portals.Add(o);
        //}
    }
	// Update is called once per frame
    /*
	void Update () {
		
	}

    public void SelectPortal()
    {
        if(portals.Count < 1)
        {
            return;
        }

        int rand = Random.Range(0, portals.Count - 1);
        
        photonView.RPC("ActivatePortal", PhotonTargets.All, rand);
    }

    [PunRPC]
    void ActivatePortal(int rand, PhotonMessageInfo info)
    {
        portals[rand].GetComponent<Portal>().ActivatePortal();
    }
    */
}
