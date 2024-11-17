using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class LeverMirrorControl : MonoBehaviour
{
    public GameObject mirror;        // Reference to the mirror GameObject
    public XRLever lever;            // Reference to the XR Lever component
    public float activationThreshold = 0.25f; // Lever value threshold for activation

    void Update()
    {
        // Get the local rotation of the lever
        float leverAngle = lever.transform.localEulerAngles.z;

        // Adjust angle for Unity's 0-360 wrapping
        if (leverAngle > 180) leverAngle -= 360;

        // Normalize the angle to a value between 0 and 1
        float normalizedValue = Mathf.InverseLerp(-45f, 45f, leverAngle);

        if (normalizedValue <= activationThreshold)
        {
            mirror.SetActive(true);
        }
        else
        {
            mirror.SetActive(false);
        }
    }

}
