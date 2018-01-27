using UnityEngine;
using UnityEngine.UI;
 
public class Launcher : Photon.PunBehaviour
    {
        
        
        
        #region Public Variables
        public Text status;
        public InputField nameR;
        public Toggle toggle;
        #endregion
 

        #region Private Variables
 
 
        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";
 
 
        #endregion
 
 
        #region MonoBehaviour CallBacks
 
 
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
 
 
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;
 
 
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }
 
 
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
		bool connecting;

        void Start()
        {
			connecting = false;
            if (!PhotonNetwork.connected)status.text = "Connect";
            Connect();
			//PhotonNetwork.JoinLobby ();

        }
		public override void OnJoinedLobby (){
			Debug.Log("joined lobby");
			//PhotonNetwork.JoinRandomRoom ();
		}
 
        #endregion
 
 
        #region Public Methods
 
 
        /// <summary>
        /// Start the connection process. 
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
			connecting = true;
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.connected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
                //PhotonNetwork.JoinRandomRoom();
                join();

            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }
 
 
    #endregion
    void join(){
        PlayerPrefs.SetString("type",toggle.isOn.ToString());
        if(nameR.text == "") nameR.text = "devRoom";
        PhotonNetwork.JoinOrCreateRoom(nameR.text, new RoomOptions() { MaxPlayers = 4 }, null);
    }
	public override void OnConnectedToMaster()
	{
		Debug.Log("connected");
        status.text = "Play";
        
		if(connecting){
			//PhotonNetwork.JoinRandomRoom();
		}
	}
		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
	    Debug.Log("failed find room");
		join();
	}
 
	public override void OnJoinedRoom()
	{
		Debug.Log("found room");
		PhotonNetwork.LoadLevel("main");
	}
 
    }
