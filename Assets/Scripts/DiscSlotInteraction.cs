using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSlotInteraction : MonoBehaviour
{
    public Transform discSlot;               // Reference to the disc slot's transform
    public AudioSource discAudioSource;      // Reference to the audio source on the disc
    public float rotationSpeed = 100f;       // Speed of the disc rotation

    private bool isInSlot = false;           // Flag to check if the disc is in the slot

    private void OnTriggerEnter(Collider other)
    {
        // Check if the disc has entered the disc slot
        if (other.transform == discSlot)
        {
            isInSlot = true;
            StartPlaying();
            Debug.Log("Disc entered the slot, playing audio.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the disc has exited the disc slot
        if (other.transform == discSlot)
        {
            isInSlot = false;
            StopPlaying();
            Debug.Log("Disc exited the slot, stopping audio.");
        }
    }

    private void Update()
    {
        // Rotate the disc while it's in the slot
        if (isInSlot)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void StartPlaying()
    {
        // Play the audio if it is not already playing
        if (!discAudioSource.isPlaying)
        {
            discAudioSource.Play();
        }
    }

    private void StopPlaying()
    {
        // Stop the audio if it is playing
        if (discAudioSource.isPlaying)
        {
            discAudioSource.Stop();
        }
    }
}
