using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Controls the world gear behaviour.
/// </summary>
public class Gear : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializeField, Range(0.05f, 2f)] private float animationDuration = 0.42f;
    
    /// <summary>
    /// Removes the gear from the world.
    /// </summary>
    public void DeSpawn() => transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.OutBack).onComplete = () => Destroy(gameObject);
}
