using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charControl : MonoBehaviour {

	public float speed = 10.0f;
	public bool inventoryActive;


	// Use this for initialization
	void Start () 
	{
		inventoryActive = false;
		Cursor.lockState = CursorLockMode.Locked;

	}
	
	// Update is called once per frame
	void Update () 
	{
		float translation = Input.GetAxis("Vertical") * speed;
		float strafe = Input.GetAxis("Horizontal") * speed;
		translation *=Time.deltaTime;
		strafe *=Time.deltaTime;

		transform.Translate(strafe, 0, translation);

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
			inventoryActive = true;
		}

	}


}
