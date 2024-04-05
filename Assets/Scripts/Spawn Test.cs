using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public GameObject enemyprefab;
    public Vector3 spawnPoint;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //spawnPoint += new Vector2(-18, 12);
        Debug.Log("Collision detected");
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            Instantiate(enemyprefab, spawnPoint, Quaternion.identity);
        }
    }

}
