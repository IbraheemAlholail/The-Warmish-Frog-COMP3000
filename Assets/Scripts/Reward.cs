using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public enum RewardType
    {
        coin,
        health,
        win,
        key,
        specialKey,
        powerUp
    }

    public int rewardAmount;
    public RewardType pickup;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public string specialKeyName;
    public powerUp powerUp;



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
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        playerHealthManager phm = FindObjectOfType<playerHealthManager>();

                        if (player.currentHealth.Runtimeval < phm.maxHealth.initialVal * 2)
                        {
                            audioSource.clip = pickupSound;
                            audioSource.Play();
                            Destroy(gameObject);
                            player.currentHealth.Runtimeval += rewardAmount;
                            if (player.currentHealth.Runtimeval > phm.maxHealth.initialVal * 2)
                            {
                                player.currentHealth.Runtimeval = phm.maxHealth.initialVal * 2;
                            }
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
                case RewardType.powerUp:
                    {
                        audioSource.clip = pickupSound;
                        audioSource.Play();
                        Destroy(gameObject);
                        PlayerMovement player = FindObjectOfType<PlayerMovement>();
                        switch (powerUp)
                        {
                            case powerUp.gun:
                                if (!player.powerUps.Contains(powerUp.gun)) player.powerUps.Add(powerUp);
                                break;
                        }
                        break;
                    }
            }
        }
    }

}
