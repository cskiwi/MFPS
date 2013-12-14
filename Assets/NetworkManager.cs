using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standByCamera;
	SpawnSpot[] _spawnSpots;
	

	// Use this for in	itialization
	void Start () {
		_spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
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
		if (_spawnSpots == null){
			Debug.LogError("Dafuq? no spawn spots?");
			return;
		}

		SpawnSpot mySpawnSpot = _spawnSpots[Random.Range(0, _spawnSpots.Length)];
		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standByCamera.enabled = false;
		
		((MonoBehaviour)myPlayer.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayer.GetComponent("MouseLook")).enabled = true;
		myPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}
}
