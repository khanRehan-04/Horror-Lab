using UnityEngine;
using UnityEngine.UI; // Import for UI components
using UnityEngine.SceneManagement; // Import for scene management
using System.Collections.Generic;

public class LevelSelectionManager : MonoBehaviour
{
    public void ContinueGame()
    {
        AudioManager.Instance.PlaySFX("click");  //Click SFX
        GameManager.Instance.LoadGameplay(); // Load gameplay scene
    }
    public void LoadNewGame()
    {
        AudioManager.Instance.PlaySFX("click");  //Click SFX
        GameManager.Instance.ResetGame();
        GameManager.Instance.LoadGameplay();
    }

}
