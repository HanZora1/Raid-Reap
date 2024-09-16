using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;  // Import UnityEvents

public class NPCInteraction : MonoBehaviour
{
    public Button npcButton;  // The button for interacting with the NPC
    public string playerTag = "Player";  // Tag the player
    public UnityEvent onPlayerEnter;  // Event for when player enters the trigger
    public UnityEvent onPlayerExit;   // Event for when player exits the trigger

    void Start()
    {
        npcButton.interactable = false;  // Initially disable the button
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            npcButton.interactable = true;  // Enable button when player is in range
            onPlayerEnter.Invoke();         // Trigger the enter event
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            npcButton.interactable = false;  // Disable button when player leaves
            onPlayerExit.Invoke();           // Trigger the exit event
        }
    }
}
