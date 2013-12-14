using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	// float lastUpdateTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			// Do Nothing -- the charmotor/input/... is moving us
		} else {
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			// this is OUR player. We need to send our actual pos to the network.
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			// This is someone else's player. We neet to recevie their pos (as of few ms ago)
			realPosition = (Vector3) stream.ReceiveNext();
			realRotation = (Quaternion) stream.ReceiveNext();
		}
	}
}
