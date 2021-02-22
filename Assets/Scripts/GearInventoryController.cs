using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Controls the spawning and reset behaviour for the UI gears.
/// </summary>
public class GearInventoryController : MonoBehaviour {
    [Header("Gear Settings")] // Lists for the prefabs and spawn points.
    [SerializeField] private List<GameObject> gearPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> gearParents = new List<Transform>();

    [Header("Animation Settings")] 
    [SerializeField, Range(0.05f, 1f)] private float gearSpawnDelay = 0.35f;
    
    // Other objects components.
    private List<GearSlotUI> uiSlots = new List<GearSlotUI>();
    private List<GearPin> gearPins = new List<GearPin>();
    private WaitForSeconds waitForSeconds;
    
    // Initialization.
    private IEnumerator Start() {
        uiSlots = FindObjectsOfType<GearSlotUI>().ToList();
        gearPins = FindObjectsOfType<GearPin>().ToList();
        waitForSeconds = new WaitForSeconds(gearSpawnDelay);

        var parents = new Queue<Transform>(gearParents);
        var gears = new Queue<GameObject>(gearPrefabs);

        foreach(var parent in parents) {
            yield return waitForSeconds;
            Instantiate(gears.Dequeue(), parent.position, parent.rotation, transform);
        }
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
        foreach(var gearPin in gearPins) gearPin.SetGear(false);

        foreach(var uiGear in FindObjectsOfType<GearUI>()) uiGear.DeSpawn();
        foreach(var slotUI in uiSlots) slotUI.SetGear(true);

        GearPinController.OnGearsReset.Invoke();
        
        for(var i = 0; i < gearParents.Count; i++) {
            if(gearPrefabs[i] == null || gearParents[i] == null) continue;
            Instantiate(gearPrefabs[i], gearParents[i].position, Quaternion.identity, transform);
        }
    }
}
