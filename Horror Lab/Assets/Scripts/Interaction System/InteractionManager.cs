using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Button handButton;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private TextMeshProUGUI interactableNameText;

    private IInteractable[] currentInteractables;

    private void Start()
    {
        // Ensure the hand button is initially disabled
        if (handButton != null)
        {
            handButton.gameObject.SetActive(false);
            handButton.onClick.AddListener(OnHandButtonPressed);
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned! Please assign it in the Inspector.");
        }

        if (interactableNameText != null)
        {
            interactableNameText.text = string.Empty;
        }
    }

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        // Perform a raycast to find interactable objects in front of the player
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRadius, interactableLayer))
        {
            // Get all IInteractable components on the hit object
            IInteractable[] interactables = hit.collider.GetComponents<IInteractable>();
            if (interactables.Length > 0)
            {
                currentInteractables = interactables;
                handButton.gameObject.SetActive(true); // Show hand button

                // Display the name of the first interactable for simplicity
                if (interactableNameText != null)
                {
                    interactableNameText.text = interactables[0].GetName();
                }
                return;
            }
        }

        // No interactable detected, reset state
        currentInteractables = null;
        handButton.gameObject.SetActive(false); // Hide hand button
        if (interactableNameText != null)
        {
            interactableNameText.text = string.Empty;
        }
    }

    private void OnHandButtonPressed()
    {
        if (currentInteractables != null)
        {
            foreach (IInteractable interactable in currentInteractables)
            {
                interactable.Interact();
            }

            // Explicitly clear the interactables and reset the UI
            currentInteractables = null;
            handButton.gameObject.SetActive(false);
            interactableNameText.text = string.Empty;
        }
        else
        {
            Dbg.LogWarning("No interactable available, but hand button was pressed!");
        }
    }

}
