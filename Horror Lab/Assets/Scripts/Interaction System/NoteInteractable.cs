using UnityEngine;

public class NoteInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameplayController gameplayController;

    [SerializeField] private string interactableName = "Note";

    public void Interact()
    {
        AudioManager.Instance.PlaySFX("paper");
        gameplayController.ShowTaskWithDelay();
    }

    public string GetName()
    {
        return $"[{interactableName}]"; // Return the name of the interactable object
    }
}
