using UnityEngine;

[System.Serializable]
public class Objective
{
    public string taskDescription;  // Description of the task
    public bool isCompleted;  // Flag to check if the task is completed
    public Vector3 playerPosition;  // Player's position when the task is completed
    public Vector3 playerRotation;  // Player's rotation when the task is completed

    // Constructor to initialize the objective
    public Objective(string description)
    {
        taskDescription = description;
        isCompleted = false;  // Tasks start as not completed
        playerPosition = Vector3.zero;  // Default to zero position
        playerRotation = Vector3.zero;  // Default to zero rotatio
    }

    // Method to mark the task as completed
    public void CompleteTask()
    {
        isCompleted = true;
    }

    // Method to set player position and rotation when completing the objective
    public void SetPlayerPositionAndRotation(Vector3 position, Vector3 rotation)
    {
        playerPosition = position;
        playerRotation = rotation;
    }
}
