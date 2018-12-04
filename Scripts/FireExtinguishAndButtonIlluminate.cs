using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguishAndButtonIlluminate : MonoBehaviour {

    public ExtinguishableFire fire;
    public TimedIlluminator illuminator;
	public Inventory inventory;

    private void OnMouseDown()
    {
		if (!inventory.InventoryContains ("Water_Bucket"))
			return;

		GetComponent<AudioSource>().Play();
        fire.Extinguish();
        illuminator.Activate();

		inventory.removeItem ("Water_Bucket");
    }

}
