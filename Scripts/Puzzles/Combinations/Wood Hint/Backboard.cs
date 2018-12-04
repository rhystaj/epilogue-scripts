using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backboard : MonoBehaviour {

    public const int EMPTY = 0;
    public const int STEAKS_PLACED = 1;
    public const int CAPS_PLACED = 2;

    public Inventory inventory;
    public GameObject caps;
    public GameObject steaks;

    int state = EMPTY;

    public int GetState()
    {
        return state;
    }

    public void PickUpCaps()
    {
        state = STEAKS_PLACED;
    }

    private void OnMouseDown()
    {
        if (state == EMPTY && inventory.InventoryContains("Steaks"))
        {
			GetComponent<AudioSource> ().Play ();

            steaks.SetActive(true);
            state = STEAKS_PLACED;
            inventory.removeItem("Steaks");
        }
        else if (state == STEAKS_PLACED && inventory.InventoryContains("Caps"))
        {

			GetComponent<AudioSource> ().Play ();

            caps.SetActive(true);
            inventory.removeItem("Caps");
            state = CAPS_PLACED;
        }
       
    }

}
