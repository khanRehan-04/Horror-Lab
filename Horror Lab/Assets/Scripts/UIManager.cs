using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text objectiveText; // TextMesh Pro element for subtitles
    public GameObject objectivePanel; // Background panel for subtitles
    public Image blackPanel; // Image component for black panel
    public TMP_Text dayText; // TextMesh Pro element for "End of Day X" message

    private void Awake()
    {
        // Ensure panel and text are hidden initially
        objectiveText.gameObject.SetActive(false);
        objectivePanel.SetActive(false);

        // Initialize black panel as invisible
        if (blackPanel != null)
        {
            Color blackColor = blackPanel.color;
            blackColor.a = 0f; // Set alpha to 0
            blackPanel.color = blackColor;
            blackPanel.gameObject.SetActive(false);
        }

        // Ensure day text is hidden initially
        if (dayText != null)
        {
            dayText.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // Start the game with a fade-out effect
        StartCoroutine(FadeOutCoroutine());
    }

    // Displays the subtitle with the objective name and message
    public void DisplaySubtitle(string objectiveName, string message)
    {
        // Format text with objectiveName styled and message on the next line
        objectiveText.text = $"<style=C2>{objectiveName}</style>\n{message}";

        // Show the text and panel
        objectivePanel.SetActive(true);
        objectiveText.gameObject.SetActive(true);
    }

    // Hides the subtitle and panel
    public void HideSubtitle()
    {
        // Hide the text and panel
        objectiveText.gameObject.SetActive(false);
        objectivePanel.SetActive(false);
    }

    // Show the black panel (fully visible)
    public void ShowBlackPanel()
    {
        if (blackPanel != null)
        {
            blackPanel.gameObject.SetActive(true);
            Color panelColor = blackPanel.color;
            panelColor.a = 1f; // Fully visible
            blackPanel.color = panelColor;
        }
    }

    // Hide the black panel (fully invisible)
    public void HideBlackPanel()
    {
        if (blackPanel != null)
        {
            Color panelColor = blackPanel.color;
            panelColor.a = 0f; // Fully invisible
            blackPanel.color = panelColor;
            blackPanel.gameObject.SetActive(false);
        }
    }

    // Fades in the black screen, displays "End of Day X", and calls LoadGameplay
    public void FadeInAndLoadGameplay(System.Action onFadeComplete = null)
    {
        AudioManager.Instance.PlaySFX("level");
        StartCoroutine(FadeInAndLoadCoroutine(onFadeComplete));
    }

    private IEnumerator FadeInAndLoadCoroutine(System.Action onFadeComplete)
    {
        if (blackPanel != null)
        {
            blackPanel.gameObject.SetActive(true);
            float fadeDuration = 2f; // Duration for fade-in effect
            Color panelColor = blackPanel.color;

            // Gradually increase alpha
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                panelColor.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
                blackPanel.color = panelColor;
                yield return null;
            }

            // Ensure black screen is fully visible
            panelColor.a = 1f;
            blackPanel.color = panelColor;

            // Display "End of Day X"
            if (dayText != null)
            {
                int dayNumber = ObjectiveManager.Instance.GetCurrentObjectiveIndex() + 1;
                dayText.text = $"End of Day {dayNumber}";
                dayText.gameObject.SetActive(true);

                // Wait for a moment to show the message
                yield return new WaitForSeconds(2f);

                // Hide the text
                dayText.gameObject.SetActive(false);
            }

            // Invoke the callback once fade-in is complete
            onFadeComplete?.Invoke();
        }
    }


    // Fades out the black screen at the start of the game
    private IEnumerator FadeOutCoroutine()
    {
        if (blackPanel != null)
        {
            blackPanel.gameObject.SetActive(true);
            float fadeDuration = 1.5f; // Duration for fade-out effect
            Color panelColor = blackPanel.color;

            // Gradually decrease alpha
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                panelColor.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
                blackPanel.color = panelColor;
                yield return null;
            }

            // Ensure black screen is fully invisible
            panelColor.a = 0f;
            blackPanel.color = panelColor;
            blackPanel.gameObject.SetActive(false);
        }
    }
}
