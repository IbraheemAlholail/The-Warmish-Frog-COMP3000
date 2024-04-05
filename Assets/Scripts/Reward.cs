using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public enum RewardType
    {
        coin,
        health,
        win,
        key,
        specialKey
    }
    public int rewardAmount;
    public RewardType pickup;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public string specialKeyName;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource = collision.GetComponentInChildren<AudioSource>();

        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            switch (pickup)
            {
                case RewardType.win:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
                        pauseMenu.onWin();
                        break;
                    }
                case RewardType.coin:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        player.coins += rewardAmount;
                        break;
                    }
                case RewardType.health:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        playerHealthManager phm = FindObjectOfType<playerHealthManager>();
                        if (player.currentHealth.Runtimeval >= phm.maxHealth.initialVal * 2)
                        {
                            return;
                        }
                        else
                        {
                            player.currentHealth.Runtimeval += rewardAmount;
                        }
                        break;
                    }
                case RewardType.key:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        player.keys += rewardAmount;
                        break;
                    }
                case RewardType.specialKey:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        player.specialKey = specialKeyName;
                        break;
                    }
            }
        }
    }

}
