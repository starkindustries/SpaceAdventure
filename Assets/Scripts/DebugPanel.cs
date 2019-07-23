using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI axisText;
    public TextMeshProUGUI rotationText;
    public TextMeshProUGUI targetRotationText;

    // Update is called once per frame
    void Update()
    {
        axisText.text = "V: " + Format(Input.GetAxis("Vertical")) + " H: " + Format(Input.GetAxisRaw("Horizontal"));
        rotationText.text = "Rotation z: " + (player.transform.rotation.eulerAngles.z);
        // targetRotationText.text = "Acceleration: " + Format(player.GetTargetRotation());
        targetRotationText.text = "Acceleration: " + Format(player.acceleration);
    }

    // Custom String Format
    private string Format(float n)
    {
        // Round number to two decimal places
        float rounded = Mathf.Round(n * 10f) / 10f;

        // Keep trailing zeros (for consistent spacing)
        string roundedString = rounded.ToString("0.0");

        // Check if negative to account for extra "-" character
        if (rounded < 0)
        {
            return roundedString;
        }
        else
        {
            return " " + roundedString;
        }
    }
}
