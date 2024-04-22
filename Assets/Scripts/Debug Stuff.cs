using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;



public class DebugStuff : MonoBehaviour
{

    public enum debugCommands
    {
        heal,
        damage,
        kill,
        teleport,
        spawnPrefab,
        activateDialogue,
        fadeScreen,
        switchGodmode,
        holdPlayer,
        pushableBlocker
    }
    public enum triggerType
    {
        onEnter,
        onExit,
        onStay,
        onButtonPress
    }

    public enum locationType
    {
        objectRelative,
        playerRelative,
        playerRandomRadius,
        absolute
    }

    public debugCommands DebugCommand;
    public triggerType TriggerType;
    public KeyCode key;
    public locationType LocationType;
    private PlayerMovement player;
    private playerHealthManager phm;
    private RoomMove rm;

    public float value1;
    public float value2;
    public float value3;
    public string textVal;
    public bool holdPlayer;
    public Vector3 coordinates;
    private Vector3 newCoords;
    public GameObject objectToUse;
    public Text textObject;
    public Image fadeScreenObj;
    public bool closeOnCommand;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (TriggerType == triggerType.onButtonPress)
        {
            if (Input.GetKeyDown(key))
            {
                doCommand();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && TriggerType == triggerType.onExit)
        {
            doCommand();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && TriggerType == triggerType.onStay)
        {
            doCommand();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && TriggerType == triggerType.onEnter)
        {
            doCommand();
        }
    }


    public void doCommand()
    {
        doCoordinates();

        switch (DebugCommand)
        {
            case debugCommands.heal:
                player = FindObjectOfType<PlayerMovement>();
                phm = FindObjectOfType<playerHealthManager>();
                player.currentHealth.Runtimeval += value1;
                break;
            case debugCommands.damage:
                player = FindObjectOfType<PlayerMovement>();
                phm = FindObjectOfType<playerHealthManager>();
                player.currentHealth.Runtimeval -= value1;
                break;
            case debugCommands.kill:
                player = FindObjectOfType<PlayerMovement>();
                phm = FindObjectOfType<playerHealthManager>();
                player.currentHealth.Runtimeval = 0;
                break;
            case debugCommands.teleport:
                player = FindObjectOfType<PlayerMovement>();
                player.transform.position = newCoords;
                break;
            case debugCommands.spawnPrefab:
                player = FindObjectOfType<PlayerMovement>();
                Instantiate(objectToUse, newCoords, Quaternion.identity);
                break;
            case debugCommands.activateDialogue:
                if (objectToUse == null || textObject == null)
                {
                    Debug.LogError("No object to use");
                    return;
                }
                else 
                { 
                    objectToUse.SetActive(true);
                    textObject.text = textVal;
                    StartCoroutine(DeactivateDialogue(closeOnCommand));
                    if (closeOnCommand)
                    {
                        closeOnCommand = false;
                    }
                    break;
                }
            case debugCommands.fadeScreen:
                if (fadeScreenObj == null)
                {
                    Debug.LogError("No fade screen object");
                    return;
                }
                else
                {
                    fadeScreenObj.gameObject.SetActive(true);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(fadeCo(fadeScreenObj, closeOnCommand));
                    if (closeOnCommand)
                    {
                        closeOnCommand = false;
                    }
                }
                break;
            case debugCommands.switchGodmode:
                player = FindObjectOfType<PlayerMovement>();
                player.godMode = !player.godMode;
                break;
            case debugCommands.holdPlayer:
                StartCoroutine(holdPlayerCo(closeOnCommand));
                if (closeOnCommand)
                {
                    closeOnCommand = false;
                }
                break;
            default:
                break;
        }
        if (closeOnCommand)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DeactivateDialogue(bool coc)
    {
        yield return new WaitForSeconds(value1);
        objectToUse.SetActive(false);
        if (coc)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator fadeCo(Image fadeScreen, bool coc)
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 0f;
        fadeScreen.color = fadeColor;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / value1;
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;

        yield return new WaitForSeconds(value2);

        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / value3;
            fadeScreen.color = fadeColor;
            yield return null;
        }
        fadeScreen.gameObject.SetActive(false);
        if (coc)
        {
            Destroy(gameObject);
        }
        else { this.GetComponent<BoxCollider2D>().enabled = true; }
    }
    IEnumerator holdPlayerCo(bool coc)
    {
        player = FindObjectOfType<PlayerMovement>();
        player.currentState = PlayerState.stunned;
        yield return new WaitForSeconds(value2);
        player.currentState = PlayerState.idle;
        if (coc)
        {
            Destroy(gameObject);
        }
    }

    public void doCoordinates()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();

        if (LocationType == locationType.objectRelative)
        {
            newCoords = Vector3.zero;
            newCoords.x = this.transform.position.x + value1;
            newCoords.y = this.transform.position.y + value2;
        }
        else if (LocationType == locationType.playerRelative)
        {
            newCoords = Vector3.zero;
            newCoords.x = player.transform.position.x + value1;
            newCoords.y = player.transform.position.y + value2;
        }
        else if (LocationType == locationType.absolute)
        {
            newCoords = coordinates;
        }
        else if (LocationType == locationType.playerRandomRadius)
        {

            // Generate random angle and distance
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Mathf.Sqrt(Random.Range(value1 * value1, value2 * value2));

            // Convert to Cartesian coordinates
            float randomX = Mathf.Cos(angle) * distance;
            float randomY = Mathf.Sin(angle) * distance;

            // Randomly flip signs
            if (Random.Range(0, 2) == 0)
                randomX = -randomX;
            if (Random.Range(0, 2) == 0)
                randomY = -randomY;

            // Set final coordinates relative to player
            newCoords = player.transform.position + new Vector3(randomX, randomY, 0);

        }
    }

}
