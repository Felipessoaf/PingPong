using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Photon.MonoBehaviour {
	public static Game instance;
	public GameObject hero,monster;

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
		//if(PhotonNetwork.room.PlayerCount>2){
		if(bool.Parse(PlayerPrefs.GetString("type"))){
			PhotonNetwork.Instantiate(this.monster.name, monster.transform.position,monster.transform.rotation, 0);
		}
		else{
			PhotonNetwork.Instantiate(this.hero.name, hero.transform.position,hero.transform.rotation, 0);
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
