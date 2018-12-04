using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackboardPickup : ObjectPickup {

    public Backboard backboard;

    private void OnMouseDown()
    {
        Place();
        backboard.PickUpCaps();
    }

}
