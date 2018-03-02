using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class PlayerScript : NetworkBehaviour {

	[SerializeField] ToggleEvent onToggleShared;
	[SerializeField] ToggleEvent OnToggleLocal;
	[SerializeField] ToggleEvent OnToggleRemote;

	public Text SpeechBubble;
	public Text PlayerName;

	[SyncVar (hook= "OnNewName")]
	public string playerName;
	[SyncVar (hook= "OnNewMessage")]
	public string chatMsg;

	public void OnNewName (string name) 
	{
		PlayerName.text = name;
		playerName = name;
	}

	public void OnNewMessage (string msg) 
	{
		SpeechBubble.text = msg;
		chatMsg = msg;
	}


	void resetTexts() {
		if (!PlayerName.text.Equals(playerName)) {
			PlayerName.text = playerName;
		}
	}


	[Command]
	public void CmdSendNameToServer(string nameToSend)
	{
		playerName = nameToSend;
	}


	[Command]
	public void CmdSendChatToServer(string chatToSend)
	{
		chatMsg = chatToSend;
	}


	void Start()
	{
		InvokeRepeating ("resetTexts", 1.0f, 3.0f);
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
