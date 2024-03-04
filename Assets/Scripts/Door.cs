using UnityEngine;

public class Door : MonoBehaviour
{
    private SpriteRenderer doorSprite;
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;
    private PolygonCollider2D doorCollider;

    private bool isOpen = false;

    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.sprite = closedDoorSprite;
        doorCollider = GetComponent<PolygonCollider2D>();
        doorCollider.enabled = true;
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            doorCollider.enabled = false;
            doorSprite.sprite = openDoorSprite;
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            doorCollider.enabled = true;
            doorSprite.sprite = closedDoorSprite;
            isOpen = false;
        }
    }
}
