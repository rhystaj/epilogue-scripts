using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Places an object from a component into the physical game world.
 */
public class ObjectPlace : AestheticSave {

	public string inventoryObject; //The name of the plank in the inventory.
	public GameObject objectToBePlaced; //The plank that will appear when it is placed.
	public Inventory inventory; //The inventory from which the object is being placed.

	private bool objectPlaced; //Set to true when the appropriate object has been placed.

	// Use this for initialization
	void Start () {
		objectToBePlaced.SetActive(false); //Plank has not been placed yet so make inactive.
	}

	/**
     * When this object is clicked, place the plank.
     */ 
	private void OnMouseDown()
	{
		//If the object has already been placed, don't place it again.
		if (objectPlaced) return;

		//If the item is not in the player's inventory, it can't be placed.
		if (!inventory.InventoryContains(inventoryObject)) return;

		PlaceObject();

	}

	/**
     *   //Place object in world and remove from inventory.
	*/
	private void PlaceObject()
	{
		objectToBePlaced.SetActive(true);
		inventory.removeItem(inventoryObject, 1);
		objectPlaced = true;
	}

	/**
     * Returns the items that have been placed with this component to the player's inventory.
     */ 
	public void ReturnItemsToPlayer()
	{
		//If an object has not been placed yet, ignore.
		if (!objectPlaced) return;

		if (inventory.AddItem(inventoryObject, 1))
		{
			Reinitialise();
		}

	}

	private void Reinitialise()
	{
		objectToBePlaced.SetActive(false);
		objectPlaced = false;
	}

	public override void RestoreAppearance()
	{
		objectToBePlaced.SetActive(true);
	}
}
