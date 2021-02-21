using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the spawning and reset behaviour for the UI gears.
/// </summary>
public class GearInventoryController : MonoBehaviour {
    [Header("Gear Settings")] // Lists for the prefabs and spawn points.
    [SerializeField] private List<GameObject> gearPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> gearParents = new List<Transform>();
    
    // Events.
    /// <summary>
    /// Invoked by the reset button in the UI. Repositions the gears in the inventory.
    /// </summary>
    public static Action OnResetInventory;

    // Initialization.
    private void Awake() {
        OnResetInventory += ResetInventory;
        
        OnResetInventory.Invoke();
    }

    // Unsubscribes from the events.
    private void OnDestroy() {
        OnResetInventory -= ResetInventory;
    }

    /// <summary>
    /// Reset the gears in position.
    /// </summary>
    private void ResetInventory() {
        foreach(var gear in FindObjectsOfType<Gear>()) {
            gear.DeSpawn();
        }
        
        foreach(var uiGear in FindObjectsOfType<GearUI>()) {
            uiGear.DeSpawn();
        }

        for(var i = 0; i < gearParents.Count; i++) {
            if(gearPrefabs[i] != null) Instantiate(gearPrefabs[i], gearParents[i]);
        }
    }
}
