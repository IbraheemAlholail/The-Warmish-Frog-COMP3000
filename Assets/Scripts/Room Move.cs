using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 camChangemax;
    public Vector2 camChangemin;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool hasTitle;
    public string titleText;
    public TextMeshProUGUI textOnScreen;
    public float displayTime;
    public Color startingTextColor;
    public int textSize;

    public float startPositionY;
    public float endPositionY;
    public float fadeInDuration;

    private Coroutine placeNameCoroutine;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            cam.minPos = camChangemin;
            cam.maxPos = camChangemax;
            collision.transform.position += playerChange;

            if (hasTitle)
            {
                StopAllCoroutines();
                placeNameCoroutine = StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        textOnScreen.enabled = true;
        textOnScreen.text = titleText;
        textOnScreen.fontSize = textSize;

        RectTransform rectTransform = textOnScreen.GetComponent<RectTransform>();
        Vector3 startPos = rectTransform.localPosition;
        startPos.y = startPositionY;
        rectTransform.localPosition = startPos;

        Color textColor = startingTextColor;
        textColor.a = 0f;
        textOnScreen.color = textColor;

        float elapsedTime = 0f;

        // Fade in and move text from startPositionY to endPositionY
        while (elapsedTime < fadeInDuration)
        {
            float t = elapsedTime / fadeInDuration;
            textColor.a = Mathf.Lerp(0f, 1f, t);
            textOnScreen.color = textColor;

            Vector3 newPos = new Vector3(startPos.x, Mathf.Lerp(startPositionY, endPositionY, t), startPos.z);
            rectTransform.localPosition = newPos;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final position and alpha are set correctly
        rectTransform.localPosition = new Vector3(startPos.x, endPositionY, startPos.z);
        textColor.a = 1f;
        textOnScreen.color = textColor;

        yield return new WaitForSeconds(displayTime);
        textOnScreen.enabled = false;
    }
}