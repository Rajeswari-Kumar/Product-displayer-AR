using UnityEngine;
using System.Collections;

public class SlideInventoryCanvas : MonoBehaviour
{
    public RectTransform panel;           // The panel that will slide (the top panel)
    public float slideDuration = 0.4f;    // Speed of slide animation
    public float hiddenY = -300f;         // How far down it goes (adjust based on height)
    public float visibleY = 0f;           // Original position when visible

    private Coroutine slideRoutine;

    private void Start()
    {
        // Set panel to visible position at start
        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, visibleY);
    }

    public void SlideDown()
    {
        // Reveal the inventory → top panel goes DOWN
        StartSlide(hiddenY);
    }

    public void SlideUp()
    {
        // Hide inventory → top panel goes UP
        StartSlide(visibleY);
    }

    private void StartSlide(float targetY)
    {
        if (slideRoutine != null)
            StopCoroutine(slideRoutine);

        slideRoutine = StartCoroutine(SlideToPosition(targetY));
    }

    IEnumerator SlideToPosition(float targetY)
    {
        float startY = panel.anchoredPosition.y;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            float newY = Mathf.Lerp(startY, targetY, t);
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, newY);

            yield return null;
        }

        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, targetY);
    }
}
