using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject hintPanel; // The panel to display hints
    public TextMeshProUGUI hintText; // TextMeshPro component to display the hint

    [Header("Hint Data")]
    private string[] hints = new string[]
    {
        "You need to find the books: one from the History section, one from the Science section, and one from the Poetry section. Check the shelves in each section carefully.",
        "The broom might be near the empty corner of library. Look around there.",
        "The lights are out! Head to the back wall of the library and find the fuse box. You might need to inspect it closely to restore the power.",
        "A key to the sealed door is hidden in the Academics section. Check carefully between the shelves that are in the center of row.",
        "You have the books! Insert them like keys in empty slots of the shelf."
    };

    public GameObject[] hintObjects; // Array of objects to activate for each objective

    void Start()
    {
        // Ensure the hint panel is disabled at the start
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }

        // Deactivate all hint objects at the start
        foreach (GameObject obj in hintObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    public void ShowHint()
    {
        AudioManager.Instance.PlaySFX("click");

        // Get the current objective index
        int currentIndex = ObjectiveManager.Instance.GetCurrentObjectiveIndex();

        // Validate the index
        if (currentIndex >= 0 && currentIndex < hints.Length)
        {
            // Activate the hint panel
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }

            // Set the hint text
            if (hintText != null)
            {
                hintText.text = hints[currentIndex];
            }

            // Activate the corresponding hint object
            if (currentIndex < hintObjects.Length && hintObjects[currentIndex] != null)
            {
                hintObjects[currentIndex].SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Invalid objective index or hint data missing!");
        }
    }

    public void HideHint()
    {
        // Hide the hint panel
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
    }
}
