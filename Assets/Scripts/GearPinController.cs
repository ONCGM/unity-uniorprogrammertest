using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the behaviour of the gear pins. Allowing them to rotate when all the gears are in place.
/// </summary>
public static class GearPinController {
    static GearPinController() {
        OnGearPlaced += AddGear;
        OnGearRemoved += RemoveGear;
        OnGearsReset += ResetGears;
    }

    private const int MAXGears = 5;
    private static List<Gear> gears = new List<Gear>();
    
    /// <summary>
    /// How many gears are in place.
    /// </summary>
    public static int GearCount => gears.Count;

    /// <summary>
    /// Invoked when a gear in placed.
    /// </summary>
    public static Action<Gear> OnGearPlaced;

    /// <summary>
    /// Invoked when all gears are in place.
    /// </summary>
    public static Action OnGearsCompleted;

    /// <summary>
    /// Invoked when a gear in removed.
    /// </summary>
    public static Action<Gear> OnGearRemoved;

    /// <summary>
    /// Invoked when the gears are reset by the UI button.
    /// </summary>
    public static Action OnGearsReset;

    /// <summary>
    /// Adds a gear from the total.
    /// </summary>
    private static void AddGear(Gear gear) {
        if(!(bool) gears.Any(g => g.gameObject == gear.gameObject)) gears.Add(gear);
        CheckGears();
    }

    /// <summary>
    /// Removes a gear from the total.
    /// </summary>
    private static void RemoveGear(Gear gear) {
        if((bool) gears.Find(g => g.gameObject == gear.gameObject)) gears.Remove(gear);
        CheckGears();
    }

    /// <summary>
    /// Checks if all gears are in place.
    /// </summary>
    private static void CheckGears() {
        if(gears.Count >= MAXGears) OnGearsCompleted.Invoke();
    }

    /// <summary>
    /// Resets the gear count.
    /// </summary>
    private static void ResetGears() => gears.Clear();
}
