using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attatched to any object that can be picked up.
 */
public class ObjectPickup : AestheticSave {

	public string inventoryObject; //The name of the object to be added to the inventory when the object is picked up.
	public Inventory inventory; //The inventory to which the item will be added when picked up.
	public int itemQuantity; //The number of items that will be added to the inventory on pickup.
	public AudioSource soundSource;

	private void OnMouseDown()
	{ 

		//Try to add the respective object to the inventory, and remove the object from the world in successful.
		if (inventory.AddItem(inventoryObject, itemQuantity)) { 
			soundSource.Play ();
			gameObject.SetActive(false);
		}

	}

	public override void RestoreAppearance()
	{
		gameObject.SetActive(false);
	}

	protected void Place()
	{
		OnMouseDown();
	}
}
