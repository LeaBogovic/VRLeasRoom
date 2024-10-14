using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RadioPickupAudio : MonoBehaviour
{
    public AudioSource radioAudio;   // Reference to the AudioSource component that will play the radio sound

    private XRGrabInteractable grabInteractable;   // Reference to the XRGrabInteractable component

    void Awake()
    {
        // Get the XRGrabInteractable component attached to the radio object
        grabInteractable = GetComponent<XRGrabInteractable>();

        // If AudioSource is not assigned in the inspector, get it from the same GameObject
        if (radioAudio == null)
        {
            radioAudio = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        // Subscribe to the events for when the radio is picked up and dropped
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        // Unsubscribe from the events to prevent memory leaks
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    // This function is called when the radio is picked up
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!radioAudio.isPlaying)
        {
            radioAudio.Play();
        }
    }

    // This function is called when the radio is dropped (optional)
    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (radioAudio.isPlaying)
        {
            radioAudio.Stop();   // Stop the audio if you want it to stop when dropped
        }
    }
}