using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform home;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    private bool hit;
    private Rigidbody2D rb;
    public Animator anim;

    public bool Hit
    {
        get { return hit; }
        set { hit = value; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentEState = eState.idle;
        anim.SetBool("wakeUp", false);
    }

    void FixedUpdate()
    {
        if (currentEState != eState.stunned)
        {
            moveLog();
        }
    }

    void moveLog()
    {
        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        if (distanceToTarget <= chaseRadius && distanceToTarget >= attackRadius)
        {
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + directionToTarget * moveSpeed * Time.deltaTime;
            changeAnim(directionToTarget);
            transform.position = newPosition;
            currentEState = eState.walk;
            anim.SetBool("wakeUp", true);
        }
        else if (distanceToTarget == attackRadius)
        {
            rb.velocity = Vector2.zero;
            currentEState = eState.walk;
            anim.SetBool("wakeUp", true);
        }
        else if (distanceToTarget > chaseRadius && transform.position != home.position)
        {
            Vector2 directionToHome = (home.position - transform.position).normalized;
            changeAnim(directionToHome);
            transform.position = Vector2.MoveTowards(transform.position, home.position, moveSpeed * Time.deltaTime);
            currentEState = eState.walk;
            anim.SetBool("wakeUp", true);
        }
        else if (transform.position == home.position)
        {
            anim.SetBool("wakeUp", false);
        }
        
    }

    private void changeEState(eState newEstate)
    {
        if(currentEState != newEstate)
        {
            currentEState = newEstate;
        }
    }

    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                setAnimFloat(Vector2.right);
            }else if(direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                setAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }

    private void setAnimFloat(Vector2 setV)
    {
        anim.SetFloat("moveX" , setV.x);
        anim.SetFloat("moveY", setV.y);
    }


}
