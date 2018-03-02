using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NameScript : NetworkBehaviour {

	public InputField mainInputField;
	public GameObject player;
	public Button button;
	public GameObject InputField;

	private Text btnTxt;


	Text [] newText ;

	void Start () {
		mainInputField.text = "";
		Button btn = button.GetComponent<Button>();
		newText = player.GetComponentsInChildren<Text> ();
		btn.onClick.AddListener(TaskOnClick);
		btnTxt = btn.GetComponentInChildren<Text> ();
	}


	void TaskOnClick()
	{
		if (btnTxt.text == "Save") 
		{
			InputField.SetActive (false);
			btnTxt.text = "Change";
			player.GetComponent<PlayerScript>().CmdSendNameToServer (mainInputField.text);
		}
		else if (btnTxt.text == "Change") 
		{
			InputField.SetActive (true);
			btnTxt.text = "Save";
		}
	}



	void Update () {

		var targetObjs = GameObject.FindGameObjectsWithTag("Player");
	
		foreach (GameObject targetObj in targetObjs) {
			if (targetObj) {
				NetworkIdentity networkIdentity = targetObj.GetComponent<NetworkIdentity> ();

				if (networkIdentity.isLocalPlayer) {

					var controls = targetObj.GetComponent<ThirdPersonUserControl> ();

					if (InputField.GetComponent<InputField>().isFocused == true)
					{
						controls.enabled = false;
					}
					else controls.enabled = true;

					newText [1].text = mainInputField.text;
				}
			}
		}
	}
}
