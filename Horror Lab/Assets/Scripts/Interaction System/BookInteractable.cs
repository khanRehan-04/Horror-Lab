using UnityEngine;
using System.Collections;
public class BookInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableName = "Book";
    [SerializeField] private Transform handPosition; // Position where the book will attach when picked up

    private bool isPickedUp = false;
    private static BookInteractable pickedUpBook = null; // Track the currently picked-up book

    public void Interact()
    {
        if (pickedUpBook != null)
        {
            Dbg.Log("A book is already being held. Please place it first.");
            return;
        }

        if (!isPickedUp)
        {
            PickUp();
        }
        else
        {
            Dbg.Log("You are already holding this book.");
        }
    }

    public string GetName()
    {
        return $"[{interactableName}]";
    }

    private void PickUp()
    {
        AudioManager.Instance.PlaySFX("pick");
        isPickedUp = true;
        pickedUpBook = this; // Mark this book as picked up
        transform.SetParent(handPosition); // Attach to the player's hand
        transform.position = handPosition.position; // Snap to the hand's position
        transform.rotation = handPosition.rotation; // Align rotation with the hand
        Dbg.Log($"{interactableName} has been picked up!");
    }

    public void Drop()
    {
        isPickedUp = false;
        pickedUpBook = null; // Release the reference to the picked-up book
        Dbg.Log($"{interactableName} has been dropped.");
    }

    public static BookInteractable GetPickedUpBook()
    {
        return pickedUpBook; // Return the currently picked-up book
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
