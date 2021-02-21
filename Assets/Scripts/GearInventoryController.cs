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
    
    // Initialization.
    private void Awake() {
        ResetInventory();
    }

    /// <summary>
    /// Resets the gears in their position on the inventory.
    /// </summary>
    public void ResetGears() => ResetInventory();
    
    /// <summary>
    /// Reset the gears in position.
    /// </summary>
    private void ResetInventory() {
        foreach(var gear in FindObjectsOfType<Gear>()) gear.DeSpawn();

        foreach(var uiGear in FindObjectsOfType<GearUI>()) uiGear.DeSpawn();

        for(var i = 0; i < gearParents.Count; i++) {
            if(gearPrefabs[i] == null) continue;
            Instantiate(gearPrefabs[i], transform).transform.position = gearParents[i].transform.position;
        }
    }
}
