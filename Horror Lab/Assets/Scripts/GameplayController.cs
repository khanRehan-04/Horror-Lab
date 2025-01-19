using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    public UIManager uiManager;
    public Transform player;


    // Array of game objects related to each task (indexed by task index)
    public GameObject[] taskRelatedObjects;

    private string objectiveName;
    private int currentTaskIndex;
    private ObjectiveData currentObjective;

    public static bool hasKey = false;

    private IEnumerator Start()
    {
        yield return null;
        currentTaskIndex = ObjectiveManager.Instance.GetCurrentObjectiveIndex();
        objectiveName = ObjectiveManager.Instance.GetCurrentObjectiveName();
        currentObjective = ObjectiveManager.Instance.objectives[currentTaskIndex];

        yield return null;

        LoadCurrentTask();
    }

    private void LoadCurrentTask()
    {
        if (ObjectiveManager.Instance != null)
        {

            if (currentTaskIndex >= 0 && currentTaskIndex < ObjectiveManager.Instance.objectives.Count)
            {
                // Debug to check if player reference is set
                if (player == null)
                {
                    return; // Exit if player is not referenced
                }

                // Set player position and rotation from the ObjectiveData
                player.position = currentObjective.defaultPosition;
                player.eulerAngles = currentObjective.defaultRotation;

                // Manage the activation of task-related game objects based on the task index
                SetTaskRelatedObjectsActive(currentTaskIndex);

                Debug.Log($"Player Position: {player.position}, Rotation: {player.eulerAngles}");
            }
            else
            {
                Debug.LogError("Current task index is out of range. Check the objectives list.");
            }
        }
    }

    public void ShowTaskWithDelay()
    {
        string taskDescription = currentObjective.taskDescription;

        uiManager.DisplaySubtitle(objectiveName, taskDescription);
    }

    private void SetTaskRelatedObjectsActive(int taskIndex, bool isActive = true)
    {
        // Check if the task index is valid and the task-related objects array is populated
        if (taskRelatedObjects != null && taskIndex >= 0 && taskIndex < taskRelatedObjects.Length)
        {
            // Set the task-related game object to active or inactive based on the task index
            GameObject taskObject = taskRelatedObjects[taskIndex];

            if (taskObject != null)
            {
                taskObject.SetActive(isActive);
            }
        }
    }

    // Reset all objectives and task objects (optional method)
    public void ResetTasks()
    {
        if (ObjectiveManager.Instance != null)
        {
            // Reset the objectives and task progress
            ObjectiveManager.Instance.ResetObjectiveProgress();

            // Deactivate all task-related objects
            foreach (var taskObject in taskRelatedObjects)
            {
                if (taskObject != null)
                {
                    taskObject.SetActive(false);
                }
            }

            // Reload the first task
            LoadCurrentTask();
        }
    }

    public void OnObjectiveComplete()
    {
        uiManager.FadeInAndLoadGameplay(() =>
        {
            StartCoroutine(WaitAndLoadGameplay());
        });
    }

    private IEnumerator WaitAndLoadGameplay()
    {
        yield return null;

        ObjectiveManager.Instance.CompleteCurrentObjective();

        yield return null;

        // Load gameplay after the wait
        GameManager.Instance.LoadGameplay();
    }


}
