using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer topDoorSprite;
    public SpriteRenderer bottomDoorSprite;
    public Sprite[] openDoorSprites;
    public Sprite[] closedDoorSprites;
    public PolygonCollider2D doorCollider;
    private PlayerMovement player;
    public AudioClip doorOpenSound;
    private AudioSource audioSource;

    private bool isOpen = false;
    public bool needsKey = false;
    public bool needsSpecialKey = false;
    public bool destroyOnUnlock = false;
    public string specialKey;

    void Start()
    {
        topDoorSprite.sprite = closedDoorSprites[0]; 
        bottomDoorSprite.sprite = closedDoorSprites[1];
        doorCollider.enabled = true;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        audioSource = player.GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (needsKey && collision.CompareTag("Player") && collision.isTrigger)
        {
            player = collision.GetComponent<PlayerMovement>();
            if (player.keys > 0)
            {
                player.keys--;
                OpenDoor();
            }
        }else if (needsSpecialKey && collision.CompareTag("Player") && collision.isTrigger)
        {
            player = collision.GetComponent<PlayerMovement>();
            if (player.specialKey == specialKey)
            {
                player.specialKey = "";
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            if (doorOpenSound != null)
            {
                audioSource.clip = doorOpenSound;
                audioSource.Play();
            }
            if (destroyOnUnlock)
            {
                Destroy(gameObject);
            }
            doorCollider.enabled = false;
            topDoorSprite.sprite = openDoorSprites[0];
            bottomDoorSprite.sprite = openDoorSprites[1];
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            doorCollider.enabled = true;
            topDoorSprite.sprite = closedDoorSprites[0];
            bottomDoorSprite.sprite = closedDoorSprites[1];
            isOpen = false;
        }
    }
}
