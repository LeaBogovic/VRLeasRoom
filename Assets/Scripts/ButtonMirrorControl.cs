using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonMirrorControl : MonoBehaviour
{
    public GameObject mirror;  // Reference to the mirror object
    private bool isMirrorActive = false;  // Track mirror state
    private XRSimpleInteractable interactable;  // Reference to the XRSimpleInteractable component

    void Awake()
    {
        // Get the XRSimpleInteractable component attached to the button
        interactable = GetComponent<XRSimpleInteractable>();

        // Make sure interactable is present
        if (interactable == null)
        {
            Debug.LogError("XRSimpleInteractable component not found on button.");
        }
    }

    void OnEnable()
    {
        // Subscribe to the interactable's on select event
        if (interactable != null)
        {
            interactable.onSelectEntered.AddListener(OnButtonPressed);
        }
    }

    void OnDisable()
    {
        // Unsubscribe when the script is disabled to avoid memory leaks
        if (interactable != null)
        {
            interactable.onSelectEntered.RemoveListener(OnButtonPressed);
        }
    }

    // Called when the button is pressed (onSelectEntered)
    private void OnButtonPressed(XRBaseInteractor interactor)
    {
        // Toggle the mirror's active state
        isMirrorActive = !isMirrorActive;
        mirror.SetActive(isMirrorActive);
    }
}
