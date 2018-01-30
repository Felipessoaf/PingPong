using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject hero,monster;

    private List<GameObject> portals;
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
        if(character=="monster"){
			PhotonNetwork.Instantiate(this.monster.name, monster.transform.position,monster.transform.rotation, 0);
		}
        if(character=="hero"){
			PhotonNetwork.Instantiate(this.hero.name, hero.transform.position,hero.transform.rotation, 0);            
			//if(ch==1) PhotonNetwork.Instantiate(this.woman.name, woman.transform.position,woman.transform.rotation, 0);
			//if(ch==2) PhotonNetwork.Instantiate(this.man.name, man.transform.position,man.transform.rotation, 0);
		}

        portals = new List<GameObject>();
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
}
