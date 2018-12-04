using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Represents an item in the player's inventory.
 */ 
[System.Serializable]
public class Item {

	private string itemName; //The name of the item, used for both visual and programatic reference.
	private string itemDesc; //A brief description of the item.
	private string itemIconPath; //The image used to represent the item in the inventory.

    /**
     * Constructor
     * Create a new item with the given name and description and load the appropriate image resource.
     */ 
    public Item(string name, string desc)
    {
        itemName = name;
        itemDesc = desc;
        itemIconPath = "ItemIcons/" + name;
    }

    /**
     * Returns the name of the item.
     */ 
    public string getName()
    {
        return itemName;
    }

    /**
     * Returns the description of the item.
     */ 
    public string getDescription()
    {
        return itemDesc;
    }

    /**
     * Returns the icon used to represent this item in the inventory.
     */ 
    public Texture2D getIcon()
    {
        return Resources.Load<Texture2D>(itemIconPath);
    }
}
