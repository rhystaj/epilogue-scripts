using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public float jumpHeight;

	private bool onGround;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		onGround = true;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {

            if (!onGround) return; 

            rb.velocity = new Vector3 (0f, jumpHeight, 0f);
			onGround = false;
		}
	}

	void OnCollisionEnter(Collision other)
    { 

		if (other.gameObject.CompareTag ("ground")) 
		{
			onGround = true;
		}
	}
}
