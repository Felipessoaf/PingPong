using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : Photon.PunBehaviour {

	[PunRPC]
    void end(PhotonMessageInfo info)
    {
		PhotonNetwork.LeaveRoom();
	}

    public void EndLocal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
