using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockForce;
    public float knockTime;
    public float damage;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pots>().destroy();
        }

        if (collision.gameObject.CompareTag("Enemies") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hitBody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (hitBody != null )
            {
                Vector2 diff = hitBody.transform.position - transform.position;
                diff = diff.normalized * knockForce;

                if (collision.gameObject.CompareTag("Enemies") && hitBody.GetComponent<Enemy>().currentEState != eState.stunned && collision.isTrigger) //Enemy Knockback
                {
                    hitBody.GetComponent<Enemy>().currentEState = eState.stunned;
                    hitBody.AddForce(diff, ForceMode2D.Impulse);
                    collision.GetComponent<Enemy>().Knock(hitBody, knockTime, damage);
                }
                if (collision.gameObject.CompareTag("Player") && hitBody.GetComponent<PlayerMovement>().currentState != PlayerState.stunned && collision.isTrigger)// Player knockback
                {
                    hitBody.GetComponent<PlayerMovement>().currentState = PlayerState.stunned;
                    hitBody.AddForce(diff, ForceMode2D.Impulse);
                    collision.GetComponent<PlayerMovement>().Knock(knockTime,damage);
                }
                
            }
        }
    }
}