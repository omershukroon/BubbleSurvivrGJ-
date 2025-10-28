using System.Collections;
using UnityEngine;
using TMPro;

public class DamageTextBehaviour : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    private Vector3 startPosition = new Vector3(56f, -9f, 0f);
    private Vector3 targetPosition = new Vector3(66f, 6f, 0f);
    private float duration = 1f;

    private void Awake()
    {
        if (damageText == null)
        {
            damageText = GetComponent<TextMeshProUGUI>();
        }

        // Ensure the text is hidden at start
        gameObject.SetActive(false);
    }

    public void ShowDamage(int damageAmount)
    {
        // Update text with damage value
        damageText.text = "-" + damageAmount.ToString();

        // Reset position and make visible
        transform.localPosition = startPosition;
        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1f);
        gameObject.SetActive(true);

        // Start movement and fade coroutine
        StartCoroutine(MoveAndFade());
    }

    private IEnumerator MoveAndFade()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate position with parabolic movement
            float t = elapsedTime / duration;

            // Parabolic interpolation - adjust the height multiplier (0.2f) to control the curve
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t) +
                                 new Vector3(0f, 20f * t * (1 - t), 0f);

            transform.localPosition = currentPos;

            // Start fading out in the second half of the animation
            if (t > 0.5f)
            {
                float alpha = 1 - ((t - 0.5f) * 2); // Fade from 1 to 0 in the second half
                damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we end at the target position
        transform.localPosition = targetPosition;

        // Hide the text
        gameObject.SetActive(false);
    }
}