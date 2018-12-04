using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutKeyInInventory : Outcome {

	public Inventory inventory; 

	public override void Activate ()
	{
		inventory.AddItem ("Key");
	}

}
