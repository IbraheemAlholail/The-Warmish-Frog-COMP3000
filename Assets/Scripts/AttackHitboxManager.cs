using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxManager : MonoBehaviour
{
    public GameObject upHitbox;
    public GameObject downHitbox;
    public GameObject leftHitbox;
    public GameObject rightHitbox;

    private PolygonCollider2D upHitboxCollider;
    private PolygonCollider2D downHitboxCollider;
    private PolygonCollider2D leftHitboxCollider;
    private PolygonCollider2D rightHitboxCollider;

    private void Start()
    {
        // Initialize references to your hitbox objects
        upHitbox = transform.Find("upHitBox").gameObject;
        downHitbox = transform.Find("downHitBox").gameObject;
        leftHitbox = transform.Find("leftHitBox").gameObject;
        rightHitbox = transform.Find("rightHitBox").gameObject;

        // Get the PolygonCollider2D components
        upHitboxCollider = upHitbox.GetComponent<PolygonCollider2D>();
        downHitboxCollider = downHitbox.GetComponent<PolygonCollider2D>();
        leftHitboxCollider = leftHitbox.GetComponent<PolygonCollider2D>();
        rightHitboxCollider = rightHitbox.GetComponent<PolygonCollider2D>();

        // Disable all hitboxes initially
        DisableAllHitboxes();
    }

    public void EnableHitboxForDirection(PlayerDirection attackDirection)
    {
        // Disable all hitboxes before enabling the specific one
        DisableAllHitboxes();

        switch (attackDirection)
        {
            case PlayerDirection.up:
                upHitboxCollider.enabled = true;
                break;
            case PlayerDirection.down:
                downHitboxCollider.enabled = true;
                break;
            case PlayerDirection.left:
                leftHitboxCollider.enabled = true;
                break;
            case PlayerDirection.right:
                rightHitboxCollider.enabled = true;
                break;
        }
    }

    public void DisableAllHitboxes()
    {
        upHitboxCollider.enabled = false;
        downHitboxCollider.enabled = false;
        leftHitboxCollider.enabled = false;
        rightHitboxCollider.enabled = false;
    }
}
