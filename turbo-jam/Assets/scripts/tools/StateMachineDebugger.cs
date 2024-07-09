using UnityEngine;

public class StateMachineDebugger : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private string currentState;

    private void Update()
    {
        if (playerMovement != null)
        {
            // Update the current state from the PlayerMovement script
            currentState = playerMovement.GetCurrentState();
        }
    }

    private void OnGUI()
    {
        // Display the current state on the screen
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 300, 40), "Current State: " + currentState, style);
    }
}
