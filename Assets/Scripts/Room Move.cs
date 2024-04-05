using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 camChangemin;
    public Vector2 camChangemax;
    
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool hasTitle;
    public bool hasFade;
    public string titleText;
    public TextMeshProUGUI textOnScreen;
    public Image fadeScreen;
    
    public float textDisplayTime;
    public float fadeDisplayTime;
    public Color TextColor;
    public int textSize;

    public float startPositionY;
    public float endPositionY;
    public float fadeInDuration;
    public float fadeOutDuration;

    public float textFadeInDuration;
    public float textFadeOutDuration;

    private Coroutine placeNameCoroutine;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (hasTitle && !hasFade)
            {
                cam.minPos = camChangemin;
                cam.maxPos = camChangemax;
                collision.transform.position += playerChange;
                StopAllCoroutines();
                placeNameCoroutine = StartCoroutine(placeNameCo());
            }
            if (hasFade)
            {
                fadeScreen.gameObject.SetActive(true);
                StartCoroutine(fadeCo(fadeScreen));
            }
        }
    }

    private IEnumerator fadeCo(Image fadeScreen)
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 0f; 
        fadeScreen.color = fadeColor;
        player.currentState = PlayerState.stunned;
        bool tp = true;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / fadeInDuration;
            fadeScreen.color = fadeColor;
            
            if (fadeColor.a >= 0.8f && tp)
            {
                cam.minPos = camChangemin;
                cam.maxPos = camChangemax;
                player.transform.position += playerChange;
                tp = false;
            }

            yield return null;
        }

        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;

        yield return new WaitForSeconds(fadeDisplayTime);        

        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / fadeOutDuration;
            fadeScreen.color = fadeColor;
            if (fadeColor.a <= 0.8f)
            {
                player.currentState = PlayerState.idle;
            }
            yield return null;
        }
        fadeScreen.gameObject.SetActive(false);
        if (hasTitle)
        {
            StopAllCoroutines();
            placeNameCoroutine = StartCoroutine(placeNameCo());
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

        Color textColor = TextColor;
        textColor.a = 0f;
        textOnScreen.color = textColor;
        
        float elapsedTime = 0f;

        while (elapsedTime < textFadeInDuration)
        {
            float t = elapsedTime / textFadeInDuration;
            textColor.a = Mathf.Lerp(0f, 1f, t);
            textOnScreen.color = textColor;

            Vector3 newPos = new Vector3(startPos.x, Mathf.Lerp(startPositionY, endPositionY, t), startPos.z);
            rectTransform.localPosition = newPos;

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        rectTransform.localPosition = new Vector3(startPos.x, endPositionY, startPos.z);
        textColor.a = 1f;
        textOnScreen.color = textColor;

        yield return new WaitForSeconds(textDisplayTime);
        textOnScreen.enabled = false;
    }
}