using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject manhero,womanhero,monster;
    public Transform[] spawnpoints,spawnpointsenemy;
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
        if(character=="monster1"){
            //instancia em todo mundo
            Vector3 pos = spawnpointsenemy[0].position;
			PhotonNetwork.Instantiate(this.monster.name, pos,monster.transform.rotation, 0);
		}
        if(character=="monster2"){
            //instancia em todo mundo
            Vector3 pos = spawnpointsenemy[1].position;
			PhotonNetwork.Instantiate(this.monster.name, pos,monster.transform.rotation, 0);
		}
        if(character=="manhero"){
            //instancia em todo mundo
            Vector3 pos = spawnpoints[0].position;
			PhotonNetwork.Instantiate(this.manhero.name, pos,manhero.transform.rotation, 0);            
			//if(ch==1) PhotonNetwork.Instantiate(this.woman.name, woman.transform.position,woman.transform.rotation, 0);
			//if(ch==2) PhotonNetwork.Instantiate(this.man.name, man.transform.position,man.transform.rotation, 0);
		}
        if(character=="womanhero"){
            //instancia em todo mundo
            Vector3 pos = spawnpoints[1].position;
			PhotonNetwork.Instantiate(this.womanhero.name, pos,womanhero.transform.rotation, 0);            
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
