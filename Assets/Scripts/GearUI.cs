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

    private void Awake() {
        gameCamera = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"Triggered the collision event with {other.name}.");
    }

    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutBack).onComplete = () => Destroy(gameObject);

    private void Update() {
        if(!stickToMouse) return;
        var mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    public void OnPointerDown(PointerEventData eventData) {
        stickToMouse = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        stickToMouse = false;
    }
}
