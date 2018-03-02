using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {

	public const int maxHealth = 3;
	public Image damageImage;
	private AudioSource playerAudio;
	bool damaged;
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.



	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	void Awake () {
		playerAudio = gameObject.GetComponent <AudioSource> ();
		damageImage = gameObject.transform.GetChild (4).transform.GetChild(0).GetComponent <Image>();
		damaged = false;
	}

	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}


	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		damaged = true;
		currentHealth -= amount;
		playerAudio.Play ();


		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;

			// called on the Server, but invoked on the Clients
			RpcRespawn();
		}
	}

	void OnChangeHealth(int health) {
		currentHealth = health;
	}


	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}
}
