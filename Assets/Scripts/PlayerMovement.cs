using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stunned,
    pulling
}

public enum PlayerDirection
{
    up, down, left, right
}

public enum powerUp
{
    gun
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerDirection direction;
    public bool godMode;
    public float moveSpeed;
    private Rigidbody2D rb2d;
    private Vector2 change;
    private Animator animator;
    public InputAction movInput;
    public InputAction pullInput;

    public floatValue currentHealth;
    public signal playerHealthSignal;

    public VectorValue startPos;

    public int coins = 0;
    public int keys = 0;
    public string specialKey = "";
    public List<powerUp> powerUps = new List<powerUp>();



    void Start()
    {
        this.gameObject.SetActive(true);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        currentState = PlayerState.walk;
        transform.position = startPos.initVal;
        playerHealthManager phm = FindObjectOfType<playerHealthManager>();
        phm.updateHealth();
    }
    private void OnEnable()
    {
        movInput.Enable();
        pullInput.Enable();
    }

    private void OnDisable()
    {
        movInput.Disable();
        pullInput.Disable();
    }



    void Update()
    {
        checkHealth();
        playerHealthManager phm = FindObjectOfType<playerHealthManager>();
        if (phm != null)
        {
            phm.updateHealth();
        }

        change = Vector2.zero;
        change = movInput.ReadValue<Vector2>();
        change.Normalize();
        change *= moveSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stunned && currentState != PlayerState.pulling)
        {
            StartCoroutine(AttackCo());
        }
        else if (pullInput.phase == InputActionPhase.Started)
        {
            currentState = PlayerState.pulling;
            UpdateAnimationAndMove();
        }
        else if (currentState == PlayerState.pulling && pullInput.phase != InputActionPhase.Started)
        {
            currentState = PlayerState.walk;
        }
        else if (currentState != PlayerState.attack && currentState != PlayerState.stunned && currentState != PlayerState.pulling)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        AttackHitboxManager hitboxManager = GetComponentInChildren<AttackHitboxManager>();
        if (hitboxManager != null && currentState != PlayerState.attack && currentState != PlayerState.stunned)
        {
            animator.SetBool("Attacking", true);
            currentState = PlayerState.attack;
            hitboxManager.EnableHitboxForDirection(direction);
        }

        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);

        if (hitboxManager != null)
        {
            hitboxManager.DisableAllHitboxes();
        }

        currentState = PlayerState.walk;
    }


    void UpdateAnimationAndMove()
    {

        if (change != Vector2.zero)
        {
            if (currentState == PlayerState.pulling)
            {
                rb2d.MovePosition(rb2d.position + change / 2);
            }
            else if (currentState != PlayerState.pulling)
            {
                rb2d.MovePosition(rb2d.position + change);
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("moving", true);
                checkDirection(change);
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public void Knock(float knockTime, float damage)
    {
        if (godMode && currentHealth.Runtimeval == 1)
        {
            Debug.Log("God mode");
            currentHealth.Runtimeval = 1;
        }
        else
        {
            Debug.Log("Knockback");
            currentHealth.Runtimeval -= damage;
            playerHealthSignal.Raise();
            checkHealth();
            if (currentHealth.Runtimeval > 0)
            {
                StartCoroutine(KnockCo(knockTime));
            }
            else //player is dead
            {
                checkHealth();
            }
        }

    }

    private bool checkHealth()
    {
        bool isDead = false;
        if (currentHealth.Runtimeval <= 0)
        {
            currentHealth.Runtimeval = 0;
            this.gameObject.SetActive(false);
            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu.onDeath();
            isDead = true;
        }
        return isDead;
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (rb2d != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb2d.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb2d.velocity = Vector2.zero;
        }
    }

    void checkDirection(Vector2 vCheck)
    {
        if ((vCheck.x == 0 && vCheck.y > 0) || (vCheck.x < 0 && vCheck.y > 0) || (vCheck.x > 0 && vCheck.y > 0))
        {
            direction = PlayerDirection.up;

        }
        else if ((vCheck.x == 0 && vCheck.y < 0) || (vCheck.x < 0 && vCheck.y < 0) || (vCheck.x > 0 && vCheck.y < 0))
        {
            direction = PlayerDirection.down;
        }
        else if (vCheck.x > 0 && vCheck.y == 0)
        {
            direction = PlayerDirection.right;
        }
        else if (vCheck.x < 0 && vCheck.y == 0)
        {
            direction = PlayerDirection.left;
        }
    }
}