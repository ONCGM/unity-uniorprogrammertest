using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the UI gear behaviour.
/// </summary>
public class GearUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [Header("Animation Settings")]
    [SerializeField, Range(0.05f, 2f)] private float animationDuration = 0.42f;

    [Header("Settings")] 
    [SerializeField] private GameObject worldGearPrefab;

    private bool stickToMouse;
    private Camera gameCamera;
    private GearSlotUI slot;

    // Initialization.
    private void Awake() {
        gameCamera = FindObjectOfType<Camera>();
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutSine);
    }

    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutSine).onComplete = () => Destroy(gameObject);

    // Updates the pointer position if the player is holding down the mouse button.
    private void Update() {
        if(!stickToMouse) return;
        var mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    // Pointer Events.
    public void OnPointerDown(PointerEventData eventData) => stickToMouse = true;
    public void OnPointerUp(PointerEventData eventData) => stickToMouse = false;
    
    /// <summary>
    /// Releases the cursor lock. 
    /// </summary>
    public void ReleaseFromCursor() => stickToMouse = false;
    
    /// <summary>
    /// Attaches to the cursor.
    /// </summary>
    public void AttachToCursor() => stickToMouse = true;

    /// <summary>
    /// Spawns a gear in the world with the same color that this gear has.
    /// </summary>
    /// <returns> A gear component.</returns>
    public Gear SpawnGear() {
        transform.DOScale(Vector3.one, animationDuration * 0.5f).SetEase(Ease.OutFlash).onComplete = () => Destroy(gameObject);
        return Instantiate(worldGearPrefab, Vector3.zero, Quaternion.identity).GetComponent<Gear>();
    }
}
