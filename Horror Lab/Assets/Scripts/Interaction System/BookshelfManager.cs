using UnityEngine;

public class BookshelfManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bookshelves; // Three bookshelves
    [SerializeField] private ParticleSystem dustParticlePrefab; // Prefab of the dust particle system
    [SerializeField] private int numberOfDustParticles = 5; // Number of dust particles to spawn
    [SerializeField] private float dustEffectRadius = 3f; // Radius for dust particle spawning
    [SerializeField] private Vector3 dustOffset = new Vector3(0, 1, 0); // Offset for dust particle position


    public void ActivateSecretDoor()
    {
        // Animate the bookshelves to go into the ground
        foreach (var bookshelf in bookshelves)
        {
            // Example animation or movement logic for the bookshelf
            StartCoroutine(MoveBookshelfToGround(bookshelf));
        }

        // Trigger multiple dust particle effects at different positions
        TriggerMultipleDustParticles();

        // Additional logic can go here if needed (e.g., opening a door, etc.)
    }

    private void TriggerMultipleDustParticles()
    {
        for (int i = 0; i < numberOfDustParticles; i++)
        {
            // Randomly pick a position within the radius
            Vector3 randomPosition = bookshelves[0].transform.position + new Vector3(
                Random.Range(-dustEffectRadius, dustEffectRadius),
                Random.Range(0, dustOffset.y),
                Random.Range(-dustEffectRadius, dustEffectRadius)
            );

            // Move the existing dust particle system to the new position
            dustParticlePrefab.transform.position = randomPosition + dustOffset;

            // Play the dust particle effect
            dustParticlePrefab.Play();

            AudioManager.Instance.PlaySFX("rumble");

            // Optional: Add a slight delay between each dust particle effect
            StartCoroutine(WaitBeforeNextParticle(0.1f)); // Delay of 0.1 seconds
        }
    }

    private System.Collections.IEnumerator WaitBeforeNextParticle(float delay)
    {
        yield return new WaitForSeconds(delay);
    }


    private System.Collections.IEnumerator MoveBookshelfToGround(GameObject bookshelf)
    {
        Vector3 targetPosition = bookshelf.transform.position - new Vector3(0, 5, 0); // Moving it 5 units down
        float duration = 4.0f; // Duration of the move
        float timeElapsed = 0;

        Vector3 initialPosition = bookshelf.transform.position;

        while (timeElapsed < duration)
        {
            bookshelf.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        bookshelf.transform.position = targetPosition; // Ensure it ends exactly at target
        bookshelf.SetActive(false); // Optionally, deactivate bookshelf after moving
    }
}
