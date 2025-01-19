using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomInteractable1 : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableName = "Key";
    [SerializeField] private Transform handPosition; // Position where the broom will attach when picked up

    public void Interact()
    {
        AudioManager.Instance.PlaySFX("key");
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
        //transform.Rotate(90, 0, 0); // Rotate the broom by 90 degrees on the X axis
    }
}
