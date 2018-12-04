using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used in conjunction with Save manager. Changes the apperanace of components, but not thier internal state.
 */ 
public abstract class AestheticSave : MonoBehaviour { 

    /**
     * Changes the look of the component to how it could be after a specific point.
     */
    public abstract void RestoreAppearance();

}
