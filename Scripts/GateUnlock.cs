using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUnlock : MonoBehaviour {

	public Inventory inventory;
    public float audioDelay;

	private void OnMouseDown(){
	
		if (!inventory.InventoryContains ("Key"))
			return;

        GetComponent<AudioSource>().PlayDelayed(audioDelay);
        inventory.removeItem("Key");
		GetComponent<Animator> ().SetBool ("Unlock", true);
		GetComponent<BoxCollider> ().enabled = false;

	}

}
