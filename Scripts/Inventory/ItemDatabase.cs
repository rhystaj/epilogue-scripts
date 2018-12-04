using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A database of all inventory items that can be used in the game. Acessed statically.
 */ 
public class ItemDatabase{

	public static Dictionary<string, Item> items = new Dictionary<string, Item>(); //Maps the name of an item to its respective item object.

	/**
     * Add items before the rest if the game is initialised.
     */ 
	static ItemDatabase(){
		AddItem(new Item("Orb","A strange blue orb... could be useful"));
		AddItem(new Item("Note1", "A note. Reads: 'Welcome. Your journey begins with a sun symbol, and moves around the tower like a clock.'"));
		AddItem(new Item("Note2", "A note. Reads: 'You won't be able to jump this. Find something to help you.'"));
		AddItem(new Item("Drawing", "A scribbled drawing... a clue, maybe?''"));
		AddItem(new Item("Plank", "A loose plank. Looks sturdy?''"));
		AddItem(new Item("Water_Bucket", "A bucket filled with water"));
		AddItem(new Item("Steaks", "A collection of wooden steaks."));
		AddItem(new Item("Caps", "A set of caps."));
		AddItem (new Item ("Key", "A key"));
	}

	/**
    * Map the the name of the item to its respective item and add it to the database.
    */
	private static void AddItem(Item item)
	{
		if (items.ContainsKey(item.getName())) throw new System.Exception("An item with that name has already been added to the datebase.");

		items[item.getName()] = item;
	}

	/**
     * Retrieves the item from the database by its name.
     */ 
	public static Item RetrieveItem(string name)
	{
		return items[name];
	}

}
