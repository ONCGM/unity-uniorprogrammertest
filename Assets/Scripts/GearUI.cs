using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the UI gear behaviour.
/// </summary>
public class GearUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [Header("Animation Settings")]
    [SerializeField, Range(0.05f, 2f)] private float animationDuration = 0.42f;

    private bool stickToMouse;
    private Camera gameCamera;
    private GearSlotUI slot;

    private void Awake() {
        gameCamera = FindObjectOfType<Camera>();
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutSine);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<GearSlotUI>() || other.GetComponent<GearSlotUI>() == slot) return;
        slot = other.GetComponent<GearSlotUI>();
        if(slot.HasGear) return;
        slot.HasGear = true;
        transform.position = slot.transform.position;
        stickToMouse = false;
    }

    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutSine).onComplete = () => Destroy(gameObject);

    private void Update() {
        if(!stickToMouse) return;
        var mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    public void OnPointerDown(PointerEventData eventData) {
        stickToMouse = true;
        if(slot != null) slot.HasGear = false;
        slot = null;
    }

    public void OnPointerUp(PointerEventData eventData) => stickToMouse = false;
}
