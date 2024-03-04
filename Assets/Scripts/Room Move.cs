using System.Collections;
using System.Collections.Generic;
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
    public GameObject textObject;
    public Text textOnObject;
    public float displayTime;
    public Color startingTextColor;
    public int textSize;

    public float startPositionY;
    public float endPositionY;
    public float fadeInDuration;

    private Coroutine placeNameCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

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
                if (placeNameCoroutine != null)
                {

                    StopAllCoroutines();
                }
                placeNameCoroutine = StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {

        textObject.SetActive(true);
        textOnObject.text = titleText;
        textOnObject.fontSize = textSize;

        Vector3 startPos = textObject.transform.position;
        startPos.y = startPositionY;
        textObject.transform.position = startPos;

        Color textColor = startingTextColor;

        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            float newY = Mathf.Lerp(startPositionY, endPositionY, elapsedTime / fadeInDuration);
            textObject.transform.position = new Vector3(startPos.x, newY, startPos.z);

            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            textColor.a = alpha;
            textOnObject.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textObject.transform.position = new Vector3(startPos.x, endPositionY, startPos.z);
        textColor.a = 1f;
        textOnObject.color = textColor;


        yield return new WaitForSeconds(displayTime);
        textObject.SetActive(false);
    }
}