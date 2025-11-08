using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public ParticleSystem PlayBubblePopEffect;
    public TextMeshProUGUI playButtonText;
    public ParticleSystem QuitBubblePopEffect;
    public TextMeshProUGUI QuitButtonText;
    public float animationDuration = 0.25f;  // כמה זמן לוקחת האנימציה

    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayButtonSequence());
    }

    private System.Collections.IEnumerator PlayButtonSequence()
    {
        // הפעל את אפקט הפיצוץ
        if (PlayBubblePopEffect != null)
        {
            PlayBubblePopEffect.Play();
        }

        // הרץ את האנימציה של הטקסט
        if (playButtonText != null)
        {
            yield return StartCoroutine(AnimateTextDisappearance(playButtonText));
        }

        // אחרי שהאנימציה נגמרת, טען את הסצנה
        SceneManager.LoadScene(1);
    }

    private System.Collections.IEnumerator AnimateTextDisappearance(TextMeshProUGUI text)
    {
        float elapsed = 0f;
        Vector3 originalScale = playButtonText.transform.localScale;
        Vector3 targetScale = originalScale * 1.3f;
        Color originalColor = playButtonText.color;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            // הגדלה הדרגתית של הטקסט
            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            // הפחתה הדרגתית של השקיפות
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            text.color = newColor;

            yield return null;
        }

        // הסתר את הטקסט אחרי האנימציה
        text.gameObject.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        StartCoroutine(QuitButtonSequence());

    }
    private System.Collections.IEnumerator QuitButtonSequence()
    {
        // הפעל את אפקט הפיצוץ
        if (QuitBubblePopEffect != null)
        {
            QuitBubblePopEffect.Play();
        }

        // הרץ את האנימציה של הטקסט
        if (QuitButtonText != null)
        {
            yield return StartCoroutine(AnimateTextDisappearance(QuitButtonText));
        }

        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
