using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerHealthManager : MonoBehaviour
{
    public Image[] playerHearts;
    public Sprite[] heartSprites;

    public floatValue maxHealth;
    public floatValue currentHealth;

    void Start()
    {
        initializeHearts();
    }

    public void initializeHearts()
    {
        for (int i = 0;  i < maxHealth.initialVal; i++)
        {
            playerHearts[i].gameObject.SetActive(true);
            playerHearts[i].sprite = heartSprites[2];
        }
        for (int i = (int)maxHealth.initialVal; i < playerHearts.Length; i++)
        {
            playerHearts[i].gameObject.SetActive(false);
        }

    }

    public void updateHealth()
    {
        initializeHearts();

        float tempHealth = currentHealth.Runtimeval / 2;

        for (int i = 0; i < maxHealth.initialVal; i++)
        {
            if (i <= tempHealth-1)
            {
                //full heart
                playerHearts[i].sprite= heartSprites[2];
            }
            else if (i >= tempHealth)
            {
                //empty heart
                playerHearts[i].sprite = heartSprites[0];
            }
            else
            {
                //half full heart
                playerHearts[i].sprite = heartSprites[1];
            }
        }
    }
}
