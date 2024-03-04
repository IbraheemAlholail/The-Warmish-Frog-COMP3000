using UnityEngine;

public class PushableRock : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private FixedJoint2D joint;
    private Vector2 pos;
    private bool isPushing = false;
    private bool isPulling = false;

    public float pushForce = 10f;
    public float maxDistanceToPush = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        joint = GetComponent<FixedJoint2D>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (isPushing)
        {
            joint.enabled = false;
            Vector2 movementDirection = (Vector2)transform.position - pos;

            if (movementDirection != Vector2.zero)
            {
                rb.AddForce(movementDirection.normalized * pushForce);
            }
        }
        if (isPulling)
        {
            joint.enabled = true;
            joint.connectedBody = playerRb;
            Debug.Log(playerRb);
        }
        pos = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("Rock")) && !collision.collider.isTrigger && player.currentState != PlayerState.pulling)
        {
            isPushing = true;
        }
        else if (player.currentState == PlayerState.pulling)
        {
            isPulling = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("Rock")) && !collision.collider.isTrigger)
        {
            isPushing = false;
            isPulling = false;
            rb.velocity = Vector2.zero;
            joint.connectedBody = null;
        }
    }
}
