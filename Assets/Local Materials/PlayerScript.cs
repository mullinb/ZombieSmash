using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class PlayerScript : NetworkBehaviour {

	[SerializeField] ToggleEvent onToggleShared;
	[SerializeField] ToggleEvent OnToggleLocal;
	[SerializeField] ToggleEvent OnToggleRemote;

	GameObject mainCamera;

	void Start()
	{

		EnablePlayer();
	}
		

	void DisablePlayer()
	{
		onToggleShared.Invoke (false);

		if (isLocalPlayer) {
			OnToggleLocal.Invoke (false);
		}
		else
			OnToggleRemote.Invoke (false);
	}

	void EnablePlayer()
	{
		onToggleShared.Invoke (true);

		if (isLocalPlayer) {
			OnToggleLocal.Invoke (true);


		}
		 else
			OnToggleRemote.Invoke (true);
	}
}
