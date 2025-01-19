using UnityEngine;
using System.Collections;

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
        ObjectiveManager.Instance.ResetObjectiveProgress();

        StartCoroutine(WaitAndLoadGameplay());

    }

    private IEnumerator WaitAndLoadGameplay()
    {
        yield return null;
        GameManager.Instance.LoadGameplay();
    }
}