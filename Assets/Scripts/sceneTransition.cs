using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{

    public string sceneDestination;
    public Vector2 playerPos;
    public VectorValue playerMem;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerMem.initVal = playerPos; 
            SceneManager.LoadScene(sceneDestination);
        }
    }

}
