using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eState
{
    idle,walk,attack,stunned
}

public class Enemy : MonoBehaviour
{
    public eState currentEState;
    public floatValue maxHealth;
    private float currentEnemyHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public bool hasReward;
    public GameObject reward;
    private float currentHealthUI;
    private float maxHealthUI;
    public Slider healthBar;
    public GameObject parentObject;
    
    

    private void Awake()
    {
        currentEnemyHealth = maxHealth.initialVal;
        currentHealthUI = maxHealth.initialVal;
        maxHealthUI = maxHealth.initialVal;
    }
    public void Knock(Rigidbody2D currentrb2d, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(currentrb2d, knockTime));
        damageTaken(damage);
    }

    private void damageTaken(float damage)
    {
        currentEnemyHealth -= damage;
        currentHealthUI = currentEnemyHealth;
        if (healthBar != null) healthBar.value = currentHealthUI / maxHealthUI;

        if (currentEnemyHealth <= 0)
        {
            if (hasReward)
            {
                reward.transform.position = this.transform.position;
                Destroy(parentObject);
                Instantiate(reward);
            }
            else
            {
                Destroy(parentObject);
            }
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
