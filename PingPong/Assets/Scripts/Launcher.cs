using UnityEngine;
using UnityEngine.UI;
 
public class Launcher : Photon.PunBehaviour
    {
        
        
        
        public Text status;
        public InputField nameR;
        public Toggle toggle;

        public string NextScene;

        string _gameVersion = "1";
        void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
        }
 		void Start()
        {
            if (!PhotonNetwork.connected) {
                status.text = "Connect";
            }
            if (PhotonNetwork.connected && PhotonNetwork.inRoom)
            {
                PhotonNetwork.LeaveRoom();  
            }
            Connect();
        }

        public void Connect()
        {
            if (PhotonNetwork.connected)
            {
                Join();

            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }
        

    void Join(){
        //PlayerPrefs.SetString("type",(Random.Range(0,2)==1?true:false).ToString());
        //PlayerPrefs.SetString("character",toggle.isOn?"monster":"manhero");
        if(nameR.text == "") nameR.text = "devRoom";
        PhotonNetwork.JoinOrCreateRoom(nameR.text, new RoomOptions() { MaxPlayers = 4 }, null);
    }
    
	public override void OnConnectedToMaster()
	{
		Debug.Log("connected");
        status.text = "Play";
	}
    public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("lobby");//"lobby"
	}
    public override void OnJoinedLobby()
	{
		//PhotonNetwork.LoadLevel(NextScene);//"lobby"
	}
    /*
	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
	    PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
        //PhotonNetwork.JoinRandomRoom();

	}
    public void QuickConnect(){
        if (PhotonNetwork.connected)
        {

            quickjoin();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }
    void quickjoin(){
        //PlayerPrefs.SetString("type",toggle.isOn.ToString());
        PhotonNetwork.JoinRandomRoom();
    }
	*/
 
    }
