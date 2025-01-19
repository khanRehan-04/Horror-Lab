using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : Singleton<ObjectiveManager>
{
    public List<ObjectiveData> objectives; // List of objectives using ScriptableObjects
    private int currentObjectiveIndex = 0; // Tracks the current objective index

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadObjectiveProgress();
    }

    // Load the saved progress and objective data
    public void LoadObjectiveProgress()
    {
        currentObjectiveIndex = PlayerPrefs.GetInt("CurrentObjectiveIndex", 0); // Load current task index
        Dbg.Log($"Loaded Current Objective Index: {currentObjectiveIndex}");

        for (int i = 0; i < objectives.Count; i++)
        {
            // Load completion status for each task
            bool isCompleted = PlayerPrefs.GetInt($"Objective_{i}_Completed", 0) == 1;
            objectives[i].isCompleted = isCompleted;

            // Load player position and rotation (fallback to defaults if not saved)
            float posX = PlayerPrefs.HasKey($"Objective_{i}_PosX") ? PlayerPrefs.GetFloat($"Objective_{i}_PosX") : objectives[i].defaultPosition.x;
            float posY = PlayerPrefs.HasKey($"Objective_{i}_PosY") ? PlayerPrefs.GetFloat($"Objective_{i}_PosY") : objectives[i].defaultPosition.y;
            float posZ = PlayerPrefs.HasKey($"Objective_{i}_PosZ") ? PlayerPrefs.GetFloat($"Objective_{i}_PosZ") : objectives[i].defaultPosition.z;
            Vector3 playerPosition = new Vector3(posX, posY, posZ);

            // Load player rotation, fallback to default if PlayerPrefs keys do not exist
            float rotX = PlayerPrefs.HasKey($"Objective_{i}_RotX") ? PlayerPrefs.GetFloat($"Objective_{i}_RotX") : objectives[i].defaultRotation.x;
            float rotY = PlayerPrefs.HasKey($"Objective_{i}_RotY") ? PlayerPrefs.GetFloat($"Objective_{i}_RotY") : objectives[i].defaultRotation.y;
            float rotZ = PlayerPrefs.HasKey($"Objective_{i}_RotZ") ? PlayerPrefs.GetFloat($"Objective_{i}_RotZ") : objectives[i].defaultRotation.z;
            Vector3 playerRotation = new Vector3(rotX, rotY, rotZ);

            Dbg.Log($"Objective {i}: Position={playerPosition}, Rotation={playerRotation}");
        }
    }

    // Save current progress and objective data
    public void SaveObjectiveProgress()
    {
        PlayerPrefs.SetInt("CurrentObjectiveIndex", currentObjectiveIndex); // Save the current objective index
        Dbg.Log($"Saved Current Objective Index: {currentObjectiveIndex}");

        for (int i = 0; i < objectives.Count; i++)
        {
            // Save completion status for each objective
            PlayerPrefs.SetInt($"Objective_{i}_Completed", objectives[i].isCompleted ? 1 : 0);

            // Save player position and rotation for each objective
            PlayerPrefs.SetFloat($"Objective_{i}_PosX", objectives[i].defaultPosition.x);
            PlayerPrefs.SetFloat($"Objective_{i}_PosY", objectives[i].defaultPosition.y);
            PlayerPrefs.SetFloat($"Objective_{i}_PosZ", objectives[i].defaultPosition.z);

            PlayerPrefs.SetFloat($"Objective_{i}_RotX", objectives[i].defaultRotation.x);
            PlayerPrefs.SetFloat($"Objective_{i}_RotY", objectives[i].defaultRotation.y);
            PlayerPrefs.SetFloat($"Objective_{i}_RotZ", objectives[i].defaultRotation.z);
        }

        PlayerPrefs.Save();
        Dbg.Log("Objective progress saved.");
    }

    // Mark the current objective as completed
    public void CompleteCurrentObjective()
    {
        if (currentObjectiveIndex < 4)
        {
            // Mark the current objective as completed
            objectives[currentObjectiveIndex].isCompleted = true;

            // Log the completion
            Dbg.Log($"Objective {currentObjectiveIndex} completed: {objectives[currentObjectiveIndex].taskDescription}");

            // Increment the current objective index
            currentObjectiveIndex++;

            // Save the updated progress
            SaveObjectiveProgress();

            if (currentObjectiveIndex < objectives.Count)
            {
                // Display the next objective if available
                DisplayObjective();
            }
            else
            {
                Dbg.Log("All objectives completed!");
            }
        }
        else
        {
            ResetObjectiveProgress();
        }
    }

    // Display the current objective description
    private void DisplayObjective()
    {
        Dbg.Log($"Current Objective: {objectives[currentObjectiveIndex].taskDescription}");
        Dbg.Log($"Player Position: {objectives[currentObjectiveIndex].defaultPosition}");
        Dbg.Log($"Player Rotation: {objectives[currentObjectiveIndex].defaultRotation}");
    }

    // Reset the objective index and completion status
    public void ResetObjectiveProgress()
    {
        // Reset current objective index to 0
        currentObjectiveIndex = 0;

        // Reset completion status for all objectives
        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].isCompleted = false;
        }

        // Save the reset progress to persist across sessions
        SaveObjectiveProgress();

        Dbg.Log("Objective progress has been reset.");
    }

    // Get the current objective index
    public int GetCurrentObjectiveIndex()
    {
        return currentObjectiveIndex;
    }
    
    public string GetCurrentObjectiveName()
    {
        return objectives[currentObjectiveIndex].objectiveName;
    }
}
