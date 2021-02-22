using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used by the gears as a anchor point in the UI so they can snap to position.
/// </summary>
public class GearSlotUI : MonoBehaviour {
    /// <summary>
    /// Does this slot already have a gear on it.
    /// </summary>
    public bool HasGear { get; private set; } = true;

    /// <summary>
    /// Updates the HasGear property. 
    /// </summary>
    public void SetGear(bool value) => HasGear = value;

    // The gear in this slot.
    private GearUI currentGear;

    // Collision events.
    private void OnTriggerEnter2D(Collider2D other) {
        if(HasGear) return;
        if(!other.GetComponent<GearUI>()) return;

        HasGear = true;
        currentGear = other.GetComponent<GearUI>();
        currentGear.ReleaseFromCursor();
        currentGear.transform.position = transform.position;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(currentGear == null) {
            HasGear = false;
            return;
        }
        
        if(other.gameObject == currentGear.gameObject) HasGear = false;
    }
}
