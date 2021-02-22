using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Responsible for the Nugget dialog behaviour.
/// </summary>
public class NuggetDialog : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializeField, Range(0.1f, 3f)] private float speakingSpeed = 1.2f;
    
    [Header("Texts")]
    [SerializeField, TextArea(1, 3)] private string startingText = "Encaixe as engrenagens em qualquer ordem!";
    [SerializeField, TextArea(1, 3)] private string taskDoneText = "Yay, parabens. Task concluida!";

    // Components.
    private TMP_Text nuggetDialogText;
    
    // Plays the text animation.
    private void Awake() {
        nuggetDialogText = GetComponentInChildren<TMP_Text>();
        Instructions();
        
        // Subscribe to events.
        GearPinController.OnGearsCompleted += TaskCompleted;
        GearPinController.OnGearsReset += Instructions;
        GearPinController.OnGearRemoved += Instructions;
    }

    // Unsubscribe from events.
    private void OnDestroy() {
        GearPinController.OnGearsCompleted -= TaskCompleted;
        GearPinController.OnGearsReset -= Instructions;
        GearPinController.OnGearRemoved -= Instructions;
    }

    /// <summary>
    /// Updates the text to the starting state line.
    /// </summary>
    private void Instructions() {
        nuggetDialogText.text = string.Empty;
        DOTween.To(() => nuggetDialogText.text, x => nuggetDialogText.text = x, startingText, speakingSpeed);
    }

    /// <summary>
    /// Updates the text to the starting state line.
    /// </summary>
    private void Instructions(Gear gear) {
        if(GearPinController.GearCount < 4) return;
        nuggetDialogText.text = string.Empty;
        DOTween.To(() => nuggetDialogText.text, x => nuggetDialogText.text = x, startingText, speakingSpeed);
    }

    /// <summary>
    /// Updates the text to the completed state line.
    /// </summary>
    private void TaskCompleted() {
        nuggetDialogText.text = string.Empty;
        DOTween.To(() => nuggetDialogText.text, x => nuggetDialogText.text = x, taskDoneText, speakingSpeed);
    }
}
