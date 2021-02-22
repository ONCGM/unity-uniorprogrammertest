using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the world gear behaviour.
/// </summary>
public class Gear : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializeField, Range(0.05f, 2f)] private float animationDuration = 0.42f;
    [SerializeField] private Vector3 slamPositionOffset = Vector3.down;
    [SerializeField] private Vector3 slamScale = new Vector3(2f, 2f, 2f);
    
    [Header("Settings")] 
    [SerializeField] private GameObject uiGearPrefab;

    // Components.
    private Camera gameCamera;
    /// <summary>
    /// The gear pin that this gear is attached to.
    /// </summary>
    public GearPin Pin { get; set; }

    private void Awake() {
        gameCamera = FindObjectOfType<Camera>();
        transform.position += slamPositionOffset;
        transform.localScale = slamScale;
        transform.DOLocalMove(Vector3.zero, animationDuration, false).SetEase(Ease.OutQuint);
        transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutQuint);
    }

    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutFlash).onComplete = () => {
        Destroy(gameObject);
        Pin.SetGear(false);
    };

    /// <summary>
    /// Spawns an UI gear and attaches it to the cursor position.
    /// </summary>
    private void SnapUiGearToMouse() {
        Instantiate(uiGearPrefab, transform.position, Quaternion.identity, FindObjectOfType<GearInventoryController>().transform).GetComponent<GearUI>().AttachToCursor();
        GearPinController.OnGearRemoved.Invoke(this);
        DeSpawn();
    }

    private void Update() {
        if(!Input.GetMouseButtonDown(0)) return;
        var mousePosition = (Vector2) gameCamera.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        if(hit.collider == null) return;
        if(hit.collider.transform == transform) SnapUiGearToMouse();
    }
}
