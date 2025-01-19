using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    private bool isContinueGame = false; // Determines whether to load Inside or Outside scene
    private string nextScene = "MainMenuScene"; // Default scene

    protected override void Awake()
    {
        base.Awake();

        // Load the saved state of isContinueGame, default to false (0)
        isContinueGame = PlayerPrefs.GetInt("ContinueGame", 0) == 1;
        Dbg.Log($"GameManager initialized. isContinueGame loaded: {isContinueGame}");
    }

    // Method to set isContinueGame to true and save it to PlayerPrefs
    public void SetContinueGame()
    {
        isContinueGame = true;
        PlayerPrefs.SetInt("ContinueGame", isContinueGame ? 1 : 0); // Save as 1 or 0
        PlayerPrefs.Save(); // Write changes to disk
        Dbg.Log($"SetContinueGame called. isContinueGame set to: {isContinueGame}");
    }

    // Load Gameplay Scene (Inside or Outside based on isContinueGame)
    public void LoadGameplay()
    {
        nextScene = "GameplayScene";
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }


    // Load Main Menu
    public void LoadMainMenu()
    {
        nextScene = "MainMenuScene";
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    // Reset Continue Game Flag and Clear PlayerPrefs
    public void ResetGame()
    {
        isContinueGame = false; // Reset the flag
        PlayerPrefs.DeleteKey("ContinueGame"); // Clear the specific key
        PlayerPrefs.DeleteAll(); // Optionally clear all PlayerPrefs
        PlayerPrefs.Save(); // Save changes
        Dbg.Log($"Game state reset. isContinueGame: {isContinueGame}. All PlayerPrefs cleared.");
    }

    // Get the next scene to load
    public string GetNextScene()
    {
        return nextScene;
    }
}
