using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private SpriteRenderer buttonSprite;
    private TextMeshPro buttonText;
    public Sprite pressedButtonSprite;
    public Sprite ButtonSprite;

    private int objectsOnButton = 0;
    public int objectsToPress = 1;

    private static bool isPressed = false;
    public bool allowPlayer = true;
    public bool SpriteIsHidden = false;
    public bool textIsHidden = false;
    public bool makeSolid = false;

    public GameObject connectedDoor;

    private void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.sprite = ButtonSprite;
        buttonText = GetComponentInChildren<TextMeshPro>();
        UpdateButtonText();
        UpdateButtonSprite();
    }

    private void Update()
    {
        UpdateButtonText();
        UpdateButtonSprite();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowPlayer)
        {
            if (collision.CompareTag("Player") && collision.isTrigger || collision.CompareTag("Rock"))
            {
                objectsOnButton++;
                if (objectsOnButton >= objectsToPress)
                {
                    isPressed = true;
                    if (makeSolid) collision.GetComponent<PushMovement>().solid = true;

                    if (connectedDoor != null)
                    {
                        connectedDoor.GetComponent<Door>().OpenDoor();
                    }
                }
            }
        }
        else
        {
            if (collision.CompareTag("Rock"))
            {
                objectsOnButton++;
                if (objectsOnButton >= objectsToPress)
                {
                    isPressed = true;
                    if (makeSolid) collision.GetComponent<PushMovement>().solid = true;
                    if (connectedDoor != null)
                    {
                        connectedDoor.GetComponent<Door>().OpenDoor();
                    }
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (allowPlayer)
        {
            if (collision.CompareTag("Player") && collision.isTrigger || collision.CompareTag("Rock"))
            {
                objectsOnButton--;
                if (objectsOnButton < objectsToPress)
                {
                    isPressed = false;
                    if (connectedDoor != null)
                    {
                        connectedDoor.GetComponent<Door>().CloseDoor();
                    }
                }
            }
        }
        else if (collision.CompareTag("Rock"))
        {
            objectsOnButton--;
            if (objectsOnButton < objectsToPress)
            {
                isPressed = false;
                if (connectedDoor != null)
                {
                    connectedDoor.GetComponent<Door>().CloseDoor();
                }
            }
        }
    }

    private void UpdateButtonText()
    {
        if (!textIsHidden)
        {
            int displayNumber = objectsToPress - objectsOnButton;
            if (displayNumber >= 0)
            {
                buttonText.text = (objectsToPress - objectsOnButton).ToString();
            }
            else if (displayNumber > objectsToPress)
            {
                buttonText.text = objectsToPress.ToString();
            }
            else
            {
                buttonText.text = "0";
            }
        }
        else
        {
            buttonText.text = "";
        }
    }

    private void UpdateButtonSprite()
    {
        if (!SpriteIsHidden)
        {
            if (isPressed)
            {
                buttonSprite.sprite = pressedButtonSprite;
            }
            else
            {
                buttonSprite.sprite = ButtonSprite;
            }
        }
        else
        {
            buttonSprite.sprite = null;
        }
    }
}
