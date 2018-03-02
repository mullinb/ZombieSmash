using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour {

	public float moveForce = 0f;
	private Rigidbody rbody;
	public Vector3 moveDir;
	public LayerMask whatIsWall;
	public float maxDistFromWall = 0f;
	private GameObject[] players;
	private GameObject target;
	float distance = Mathf.Infinity;


	void Start () {
		rbody = GetComponent <Rigidbody> ();
		InvokeRepeating ("findPlayers", 1.0f, 2.0f);
	}

	void findPlayers () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject go in players) 
		{
			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) 
			{
				target = go;
				distance = curDistance;
				//Debug.Log(closest.name);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		rbody.velocity = moveDir * moveForce;
		if (target) {
			transform.LookAt (target.transform);
			rbody.velocity = transform.forward * moveForce;
		}
		if (Physics.Raycast (transform.transform.position, transform.forward, maxDistFromWall, whatIsWall)) 
		{
			moveDir = ChooseDirection ();
			transform.rotation = Quaternion.LookRotation(moveDir);
		}
	}

	Vector3 ChooseDirection ()
	{
		System.Random ran = new System.Random ();
		int i = ran.Next (0, 3);
		Vector3 temp = new Vector3();

		if (i == 0) {
			temp = transform.forward;
		} else if (i == 1) {
			temp = -transform.forward;
		} else if (i == 2) {
			temp = transform.right;
		} else if (i == 3) {
			temp = -transform.right;
		}
		return temp;
	}

	void OnCollisionEnter(Collision collision) {
		var hit = collision.gameObject;
		if (hit.tag == "Player") {
			var health = hit.GetComponent<Health> ();
			if (health != null) { 
				health.TakeDamage (1);
			}
		}
	}
}
