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
    public enum DoorType
    {
        key,
        enemy,
        secretKey
    }

    private bool isOpen = false;
    public DoorType thisDoorType;
    public bool destroyOnUnlock = false;
    public string specialKey;
    public GameObject enemyToDefeat;

    void Start()
    {
        topDoorSprite.sprite = closedDoorSprites[0]; 
        bottomDoorSprite.sprite = closedDoorSprites[1];
        doorCollider.enabled = true;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        audioSource = player.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (thisDoorType == DoorType.enemy)
        {
            if (enemyToDefeat == null)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (thisDoorType)
        {
            case DoorType.key when collision.CompareTag("Player") && collision.isTrigger:
                player = collision.GetComponent<PlayerMovement>();
                if (player.keys > 0)
                {
                    player.keys--;
                    OpenDoor();
                }
                break;
            case DoorType.secretKey when collision.CompareTag("Player") && collision.isTrigger:
                player = collision.GetComponent<PlayerMovement>();
                if (player.specialKey == specialKey)
                {
                    player.specialKey = "";
                    OpenDoor();
                }
                break;
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
