using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the world gear behaviour.
/// </summary>
public class Gear : MonoBehaviour, IPointerDownHandler {
    [Header("Animation Settings")]
    [SerializeField, Range(0.05f, 2f)] private float animationDuration = 0.42f;
    
    [Header("Settings")] 
    [SerializeField] private GameObject uiGearPrefab;
    
    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutFlash).onComplete = () => Destroy(gameObject);

    /// <summary>
    /// Spawns an UI gear and attaches it to the cursor position.
    /// </summary>
    private void SnapUiGearToMouse() {
        Instantiate(uiGearPrefab, transform.position, Quaternion.identity).GetComponent<GearUI>().AttachToCursor();
        DeSpawn();
    }

    
    // Click Events.
    public void OnPointerDown(PointerEventData eventData) {
        SnapUiGearToMouse();
    }
}
