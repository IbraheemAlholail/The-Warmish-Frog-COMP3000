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

    public static bool isPressed = false;

    public GameObject connectedDoor;

    private void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.sprite = ButtonSprite;

        buttonText = GetComponentInChildren<TextMeshPro>();
        UpdateButtonText();
    }

    private void Update()
    {
        UpdateButtonText();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger || collision.CompareTag("Rock"))
        {
            
            objectsOnButton++;
            
            UpdateButtonText();
            if (objectsOnButton >= objectsToPress)
            {
                buttonSprite.sprite = pressedButtonSprite;
                isPressed = true;

                if (connectedDoor != null)
                {
                    connectedDoor.GetComponent<Door>().OpenDoor();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger || collision.CompareTag("Rock"))
        {
            objectsOnButton--;
            UpdateButtonText();
            if (objectsOnButton < objectsToPress)
            {
                buttonSprite.sprite = ButtonSprite;
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
        int displayNumber = objectsToPress - objectsOnButton;
        if (displayNumber >= 0)
        {
            buttonText.text = (objectsToPress-objectsOnButton).ToString(); 
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
}
