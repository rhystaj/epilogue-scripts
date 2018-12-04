using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour {

    public Inventory inventory; //The inventory that is being drawn.

    public Vector2 positionOnScreen; //The screen position of the top left corner of the GUI.
    public Vector2 slotOffsets; //The distance between the top left of each if the slots.

    public RawImage itemSlot; //The images prefab used for the slots that hold items.

    public Vector2 iconSize;

    private List<RawImage> slotsDrawn = new List<RawImage>();
    private List<RawImage> itemsDrawn = new List<RawImage>();

    private void OnEnable()
    {
        inventory.DrawInventory += this.UpdateInventory;
    }

    private void OnDisable()
    {
        inventory.DrawInventory -= this.UpdateInventory;
    }

    private void Start()
    {
        UpdateInventory(null);
    }

    /**
     * Draws the prefab for the slot at the given position.
     */ 
    private void DrawSlot(RawImage slotImage, Vector2 position)
    {
        //Create clone from prefab
        RawImage image = Instantiate(slotImage) as RawImage;

        //Position in canvas.
        image.transform.SetParent(transform);

        RectTransform rt = image.GetComponent<RectTransform>();

        //Set the anchor to the top left corner of the screen.
        SetInventoryElementPosition(rt, position);

        slotsDrawn.Add(image);

    }

    /**
     * Updates the visual representation of the inventory based on the item selected, and the 
     */
    private void UpdateInventory(List<Texture2D> icons)
    {

        if (icons == null) return;

        //Clear what has already been drawn.
        foreach (RawImage slot in slotsDrawn) Destroy(slot);
        foreach (RawImage item in itemsDrawn) Destroy(item);

        for(int i = 0; i < icons.Count; i++)
        {
            DrawSlot(itemSlot, GetPositionOfSlot(i)); //Draw the slot for the item to go into.

            RawImage itemImage = Instantiate(itemSlot) as RawImage; //Make a clone of any image, it will be changed.
            itemImage.texture = icons[i]; //Set the image as the icon.
            itemImage.transform.SetParent(transform);

            //Position and resize the item icon.
            RectTransform iconTransform = itemImage.GetComponent<RectTransform>();
            iconTransform.sizeDelta = iconSize;
            SetInventoryElementPosition(iconTransform, GetPositionOfSlot(i));
           

            itemsDrawn.Add(itemImage);

        }


    }

    /**
     * Gets the screen position of the slot with the given number.
     */ 
    private Vector2 GetPositionOfSlot(int slot)
    {
        return new Vector2(positionOnScreen.x + slot * slotOffsets.x,
                           positionOnScreen.y + slot * slotOffsets.y);
    }

    private void SetInventoryElementPosition(RectTransform rt, Vector2 position)
    {
        rt.anchorMax = new Vector2(0, 1);
        rt.anchorMin = new Vector2(0, 1);
        rt.anchoredPosition = position;
    }
}
