using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standByCamera;

	// Use this for initialization
	void Start () {
		Connect();
	}
	void  Connect() {
		// PhotonNetwork.offlineMode = true;
		PhotonNetwork.ConnectUsingSettings("0.1.0");
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("Joined Loby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log("Failed to join random room");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom(){
		Debug.Log("Joined room");

		SpawnMyPlayer();
	}

	void SpawnMyPlayer(){
		PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0);
		standByCamera.enabled = false;
	}
}
