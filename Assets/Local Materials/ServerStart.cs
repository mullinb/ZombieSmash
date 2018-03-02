using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerStart : NetworkBehaviour {

	public NetworkManager networkManager;

	void Start () {
		networkManager.StartServer();	
	}
}
