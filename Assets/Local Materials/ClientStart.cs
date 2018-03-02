using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientStart : NetworkBehaviour {

	public NetworkManager networkManager;

	void Start () {
		networkManager.StartClient ();
	}

	// Update is called once per frame
	void Update () {

	}
}
