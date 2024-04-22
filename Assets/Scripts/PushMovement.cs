using UnityEngine;

public class PushMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Vector2 lastPosition;
    private bool isPushing = false;
    private bool isPulling = false;
    Vector2 change;


    public float pushForce = 10f;
    public float maxDistanceToPush = 2f;
    public float minDistanceToPull = 2f;

    public bool solid = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (solid) rb.bodyType = RigidbodyType2D.Static;
        checkForPull();

        if (isPushing)
        {
           //do nothing
        }
        else if (isPulling)
        {
            PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            change = Vector2.zero;
            change.x = horizontalInput;
            change.y = verticalInput;
            change.Normalize();
            change *= playerMovement.moveSpeed * Time.fixedDeltaTime;

            if (change != Vector2.zero)
            {
                rb.MovePosition(rb.position + change/2);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        lastPosition = transform.position;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("Rock")) && !collision.collider.isTrigger)
        {
            isPushing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("Rock")) && !collision.collider.isTrigger)
        {
            isPushing = false;
            rb.velocity = Vector2.zero;
        }
    }
    private void checkForPull()
    {
        if (Vector2.Distance(playerTransform.position, lastPosition) <= minDistanceToPull && Input.GetButton("pull"))
        {
            if (Vector2.Distance(playerTransform.position, lastPosition) > minDistanceToPull)
            {
                isPulling = false;
            }
            isPulling = true;
        }
        else
        {
            isPulling = false;
        }
    }
}
