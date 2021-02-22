using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPin : MonoBehaviour {
    /// <summary>
    /// Does this slot already have a gear on it.
    /// </summary>
    public bool HasGear { get; private set; } = true;

    // The gear in this slot.
    private Gear currentGear;

    // Collision events.
    private void OnTriggerEnter2D(Collider2D other) {
        if(HasGear) return;
        if(!other.GetComponent<GearUI>()) return;
        currentGear = other.GetComponent<GearUI>().SpawnGear();
        currentGear.transform.position = transform.position;
        HasGear = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(currentGear == null) {
            HasGear = false;
            return;
        }
        
        if(other.gameObject == currentGear.gameObject) HasGear = false;
    }
}
