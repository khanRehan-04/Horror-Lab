using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> lamps; // List of lamp GameObjects
    public GameplayController gameplayController;

    public void Interact()
    {
        AudioManager.Instance.PlaySFX("switch");
        AudioManager.Instance.PlaySFX("light");
        ActivateAllLamps();
    }

    private void ActivateAllLamps()
    {
        foreach (GameObject lamp in lamps)
        {
            if (lamp != null)
            {
                lamp.SetActive(true); // Activate each lamp in the list
            }
        }

        // Start a coroutine to call OnObjectiveComplete after 5 seconds
        StartCoroutine(CompleteObjectiveAfterDelay());
    }

    private IEnumerator CompleteObjectiveAfterDelay()
    {
        yield return new WaitForSeconds(5);
        gameplayController.OnObjectiveComplete(); // Call the method after the delay
    }

    public string GetName()
    {
        return "[Light Switch]"; // Return the interactable name in a formatted string
    }
}
