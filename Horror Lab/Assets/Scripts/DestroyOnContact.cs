using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the player tag
        if (other.CompareTag(playerTag))
        {
            Destroy(gameObject); // Destroy this GameObject
        }
    }
}
