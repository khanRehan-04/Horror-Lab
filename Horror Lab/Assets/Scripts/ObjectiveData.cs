using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "ScriptableObjects/ObjectiveData", order = 1)]
public class ObjectiveData : ScriptableObject
{
    public string objectiveName;

    [TextArea(3, 10)]
    public string taskDescription; // Description of the task

    public Vector3 defaultPosition; // Default player position for the objective
    public Vector3 defaultRotation; // Default player rotation for the objective
    public bool isCompleted = false; // Track completion status of the task
}
