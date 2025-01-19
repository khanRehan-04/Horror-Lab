using System;
using UnityEngine;
using System.Collections;

public class BoxInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactableName = "Box";
    [SerializeField] private Transform[] placementPositions; // Positions where books will be placed inside the box
    private int currentPlacementIndex = 0; // To track where the next book will go for this box

    public GameplayController gameplayController;
    public BookshelfManager bookshelfManager; // Reference to the BookshelfManager

    // Static variables for global book placement tracking
    private static int totalBooksPlaced = 0; // Tracks total books placed across all boxes
    private static int totalBooksRequired = 3; // Total books needed to activate the secret door

    void Start()
    {
        if(ObjectiveManager.Instance.GetCurrentObjectiveIndex() == 4)
        {
            currentPlacementIndex = 0;
            totalBooksPlaced = 0;
        }
    }

    public void Interact()
    {
        // If there is a picked-up book, place it in the box
        BookInteractable pickedUpBook = BookInteractable.GetPickedUpBook();
        if (pickedUpBook != null && currentPlacementIndex < placementPositions.Length)
        {
            AudioManager.Instance.PlaySFX("drop");
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
        currentPlacementIndex++; // Move to the next position for this box
        Dbg.Log($"{book.GetName()} has been placed inside the box.");

        // Increment the global count of books placed
        totalBooksPlaced++;
        Dbg.Log($"Total books placed: {totalBooksPlaced}/{totalBooksRequired}");

        // Check if all books have been placed
        if (totalBooksPlaced >= totalBooksRequired)
        {
            OnAllBooksPlaced();
        }
    }

    private void OnAllBooksPlaced()
    {
        if(ObjectiveManager.Instance.GetCurrentObjectiveIndex() == 0)
        {
            gameplayController.OnObjectiveComplete();
        }
        else
        {
            // Activate the secret door
            bookshelfManager.ActivateSecretDoor();

            // Call GameplayController to complete the objective after a delay
            StartCoroutine(CompleteObjectiveAfterEffects());
        }
    }

    // Coroutine to wait for bookshelf effects to finish before completing the objective
    private IEnumerator CompleteObjectiveAfterEffects()
    {
        yield return new WaitForSeconds(2);
        gameplayController.OnObjectiveComplete();
    }
}
