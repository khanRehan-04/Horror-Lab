using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableName = "Broom";
    [SerializeField] private Transform handPosition; // Position where the broom will attach when picked up

    public void Interact()
    {
        AudioManager.Instance.PlaySFX("pick");
        GameplayController.hasKey = true;
        PickUp();
    }

    public string GetName()
    {
        return $"[{interactableName}]";
    }

    private void PickUp()
    {
        transform.SetParent(handPosition); // Attach to the player's hand
        transform.position = handPosition.position; // Snap to the hand's position
        transform.rotation = handPosition.rotation; // Align rotation with the hand

        // Apply additional transformations
        transform.Rotate(90, 0, 0); // Rotate the broom by 90 degrees on the X axis
        transform.localScale = new Vector3(2, 45, 2); // Set the scale of the broom

        Dbg.Log($"{interactableName} has been picked up!");
    }
}
