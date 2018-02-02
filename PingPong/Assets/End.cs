using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : Photon.PunBehaviour {

	[PunRPC]
    void end(PhotonMessageInfo info){
		PhotonNetwork.LeaveRoom();
	}
}
