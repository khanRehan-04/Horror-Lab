using UnityEngine;
using System.Collections;

public class DustInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem dustParticleEffect; // Reference to the particle system for dust
    private float effectDuration = 1.5f; // Duration to play the effects
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Transform broomTransform; // Reference to the broom's transform

    private bool isInteracting = false; // Flag to check if interaction has started
    private static int remainingDustObjects = 10; // Static counter for tracking dust objects

    public GameplayController gameplayController;

    public void Interact()
    {
        if (!isInteracting && GameplayController.hasKey)
        {
            AudioManager.Instance.PlaySFX("swoosh");
            StartCoroutine(PlayDustEffect()); // Start dust effect and sound
        }
    }

    private IEnumerator PlayDustEffect()
    {
        isInteracting = true; // Set the interaction flag

        // Play the dust particle effect
        if (dustParticleEffect != null)
        {
            dustParticleEffect.Play();
        }

        // Wait for the effect duration
        yield return new WaitForSeconds(effectDuration);

        // Stop the effects after the specified duration
        if (dustParticleEffect != null)
        {
            dustParticleEffect.Stop();
        }

        isInteracting = false; // Reset the interaction flag
        CheckAllDustCleared(); // Check if all dust objects are cleared
    }

    private void CheckAllDustCleared()
    {
        remainingDustObjects--;

        if (remainingDustObjects <= 0)
        {
            StartCoroutine(ShowLevelCompleteWithDelay());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ShowLevelCompleteWithDelay()
    {
        GameplayController.hasKey = false;  
        yield return new WaitForSeconds(1f); // Wait for 1 second
        gameplayController.OnObjectiveComplete();
    }

    public string GetName()
    {
        return "[Spider Web]"; // Return the interactable name in a formatted string
    }
}
