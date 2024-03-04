using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eState
{
    idle,walk,attack,stunned
}

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public eState currentEState;
    public floatValue maxHealth;
    private float currentEnemyHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        currentEnemyHealth = maxHealth.initialVal;
    }
    public void Knock(Rigidbody2D currentrb2d, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(currentrb2d, knockTime));
        damageTaken(damage);
    }

    private void damageTaken(float damage)
    {
        currentEnemyHealth -= damage;
        if (currentEnemyHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D currentrb2d, float knockTime)
    {
        if (currentrb2d != null)
        {
            yield return new WaitForSeconds(knockTime);
            currentrb2d.velocity = Vector2.zero;
            currentEState = eState.idle;
        }
    }


}
