using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public const int SLOT = 1; //The number of slots in the inventory.

    public List<Item> inventory = new List<Item>(); //The items in the inventory at any given time.
    public List<int> itemQuantities = new List<int>(); //The amount of items of each type in the inventory.

    public delegate void DrawCall(List<Texture2D> icons);
    public DrawCall DrawInventory;

    private int slotSelected;

    private bool showToolTip;
    private string toolTip;
    public GUISkin skin;

    /**
     * Returns a copy of the items array.
     */ 
    public List<Item> GetItemsList()
    {
        return this.inventory;
    }

    /**
     * Returns a copy of the array of quantities.
     */ 
    public List<int> GetQuantitiesArray()
    {
        return this.itemQuantities;
    }

    /**
     * Stores the given items in the inventory.
     */ 
    public void SetItems(List<Item> items)
    {
        inventory = items;
        if (this.DrawInventory != null) this.DrawInventory(GetImageIcons());
    }

    /**
     * Sets the quantities of items to the given quantities.
     */ 
    public void SetQuantities(List<int> quantities)
    {
        itemQuantities = quantities;
    }

    /**
     * Returns the item in the selected slot.
     */
    public Item getSelectedItem() { return inventory[slotSelected - 1]; }

    /**
     * One one item of the given name to an empty space in the inventory, if there is one.
     */
    public bool AddItem(string name)
    {
        return AddItem(name, 1);
    }

    /**
     * Add the the given amount of items with the given name to an empty slot if there is one.
     * Update any GUI's subscribed to this inventory.
     * Return whether or not the item was successfully added.
     */
    public bool AddItem(string name, int quantity)
    {
        for (int i = 0; i < inventory.Count; i++)
        {

            //Can not add an item to a non-empty slot.
            if (inventory[i] != null)
            {
                //If item is already in inventory, increase number of items by the quantity.
                if (inventory[i].getName().Equals(name)) itemQuantities[i] += quantity;
                else continue; //Else slot has other item, so check next slot.

                if(DrawInventory != null) DrawInventory(GetImageIcons());
                return true;
            }

            inventory[i] = ItemDatabase.RetrieveItem(name);
            itemQuantities[i] = quantity;

            if (DrawInventory != null) DrawInventory(GetImageIcons());
            return true;

        }

        inventory.Add(ItemDatabase.RetrieveItem(name));
        itemQuantities.Add(quantity);

        DrawInventory(GetImageIcons());

        return true;
    }

    /**
     * Returns whether or not an item with the given name is being held in the inventory.
     */
    public bool InventoryContains(string name)
    {

        //Iterate over the inventory and return true if an item with the given name is found.
        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i] != null && inventory[i].getName().Equals(name)) return true;

        //If the item isn't found.
        return false;

    }

    public bool InventoryEmpty()
    {
        return inventory[0] == null;
    }

    /**
     * Removes one of the item with the given name from the inventory, if the inventory contains it.
     */
    public void removeItem(string name)
    {
        removeItem(name, 1);
    }

    /**
     * Removes the given quantity of the item with the given name, if the inventory contains it.
     * Update and GUI's subscribed to the inventory.
     */
    public void removeItem(string name, int quantity)
    {

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].getName().Equals(name))
            {

                itemQuantities[i] -= quantity;
                if(itemQuantities[i] <= 0)
                {
                    inventory.RemoveAt(i);
                    itemQuantities.RemoveAt(i);

                    DrawInventory(GetImageIcons());
                    return;
                }
                
            }
        }

    }

    /**
     * Generates an array of the icons for the items in the inventory.
     */
     private List<Texture2D> GetImageIcons()
    {
        List<Texture2D> itemIcons = new List<Texture2D>();

        foreach (Item item in inventory)
            itemIcons.Add(item.getIcon());

        return itemIcons;

    }

}
