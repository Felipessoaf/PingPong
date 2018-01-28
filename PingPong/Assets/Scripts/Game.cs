using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject woman,man,monster;

    private List<GameObject> portals;
	// Use this for initialization
	void Start () {
		
		if(instance){
			 Destroy(this);
		}
		else{
			instance = this;
            begin();
        }
	}
	public void begin(){
		int ch =  PlayerPrefs.GetInt("character");
		if(ch>2){
			PhotonNetwork.Instantiate(this.monster.name, monster.transform.position,monster.transform.rotation, 0);
		}
		else{
			if(ch==1) PhotonNetwork.Instantiate(this.woman.name, woman.transform.position,woman.transform.rotation, 0);
			if(ch==2) PhotonNetwork.Instantiate(this.man.name, man.transform.position,man.transform.rotation, 0);
		}

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Portal"))
        {
            portals.Add(o);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectPortal()
    {
        int rand = Random.Range(0, portals.Count - 1);
        
        photonView.RPC("ActivatePortal", PhotonTargets.All, rand);
    }

    [PunRPC]
    void ActivatePortal(int rand, PhotonMessageInfo info)
    {
        portals[rand].GetComponent<Portal>().ActivatePortal();
    }
}
