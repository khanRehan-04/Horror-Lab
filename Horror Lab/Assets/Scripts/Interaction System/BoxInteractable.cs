using UnityEngine;

public class BoxInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableName = "Box";
    [SerializeField] private Transform[] placementPositions; // Positions where books will be placed inside the box
    private int currentPlacementIndex = 0; // To track where the next book will go

    public void Interact()
    {
        // If there is a picked-up book, place it in the box
        BookInteractable pickedUpBook = BookInteractable.GetPickedUpBook();
        if (pickedUpBook != null && currentPlacementIndex < placementPositions.Length)
        {
            PlaceBookInBox(pickedUpBook);
        }
        else
        {
            Dbg.Log("Either no book is picked up, or the box is full.");
        }
    }

    public string GetName()
    {
        return $"[{interactableName}]";
    }

    private void PlaceBookInBox(BookInteractable book)
    {
        book.transform.SetParent(transform); // Attach the book to the box
        book.transform.position = placementPositions[currentPlacementIndex].position; // Position the book inside the box
        book.transform.rotation = placementPositions[currentPlacementIndex].rotation; // Align rotation with the placement position
        book.Drop(); // Mark the book as not picked up anymore
        currentPlacementIndex++; // Move to the next position for the next book
        Dbg.Log($"{book.GetName()} has been placed inside the box.");
    }
}
