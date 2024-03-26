using UnityEngine;

public class CanvasAspectRatio : MonoBehaviour
{
    // Desired aspect ratio (width:height)
    public float targetAspectRatio = 4f / 3f;

    void Start()
    {
        // Calculate the desired width and height based on the target aspect ratio
        float targetWidth = Screen.height * targetAspectRatio;
        float currentWidth = Screen.width;
        float currentHeight = Screen.height;

        // Check if current aspect ratio is wider than the target
        if (currentWidth / currentHeight > targetAspectRatio)
        {
            // Calculate the width to be adjusted
            float scaledWidth = currentHeight * targetAspectRatio;
            // Calculate the difference in width
            float barWidth = (currentWidth - scaledWidth) / 2f;
            // Set the anchor position to the center
            GetComponent<RectTransform>().offsetMin = new Vector2(barWidth, 0f);
            GetComponent<RectTransform>().offsetMax = new Vector2(-barWidth, 0f);
        }
        else // Current aspect ratio is narrower than the target
        {
            // Calculate the height to be adjusted
            float scaledHeight = currentWidth / targetAspectRatio;
            // Calculate the difference in height
            float barHeight = (currentHeight - scaledHeight) / 2f;
            // Set the anchor position to the center
            GetComponent<RectTransform>().offsetMin = new Vector2(0f, barHeight);
            GetComponent<RectTransform>().offsetMax = new Vector2(0f, -barHeight);
        }
    }
}
