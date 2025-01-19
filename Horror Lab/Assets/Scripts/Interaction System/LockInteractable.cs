using UnityEngine;

public class LockInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform doorPivot; // Reference to the door's pivot transform (empty GameObject)
    private float rotationAngle = 100f; // The amount to rotate the door
    [SerializeField] private float rotationSpeed = 2f; // Speed at which the door rotates

    private bool isOpening = false; // Flag to check if the door is already opening
    private float currentRotation = 0f; // Tracks how much the door has rotated

    public GameplayController gameplayController;

    public void Interact()
    {
        if (GameplayController.hasKey && !isOpening)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private System.Collections.IEnumerator OpenDoor()
    {

        isOpening = true;

        float targetRotation = currentRotation + rotationAngle; // Calculate the target rotation
        float startRotation = currentRotation;
        float elapsedTime = 0f;
        AudioManager.Instance.PlaySFX("door");
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            currentRotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime);
            doorPivot.localRotation = Quaternion.Euler(0, currentRotation, 0); // Apply rotation on Y-axis
            yield return null;
        }

        currentRotation = targetRotation; // Ensure final rotation matches the target
        doorPivot.localRotation = Quaternion.Euler(0, currentRotation, 0);

        isOpening = false;

        yield return new WaitForSeconds(2);

        gameplayController.OnObjectiveComplete();
    }

    public string GetName()
    {
        return "[Lock]"; // Return the interactable name in a formatted string
    }
}
