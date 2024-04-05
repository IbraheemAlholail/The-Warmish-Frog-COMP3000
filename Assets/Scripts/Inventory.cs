using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public TextMeshProUGUI CoinTextMesh;
    public TextMeshProUGUI KeyTextMesh;
    public TextMeshProUGUI SpecialKeyTextMesh;

    void Update()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        CoinTextMesh.text = "Coins: " + player.coins;
        KeyTextMesh.text = "Keys: " + player.keys;

        if (player.specialKey != "")
        {
            SpecialKeyTextMesh.text = "S Key: " + player.specialKey;
        }
        else
        {
            SpecialKeyTextMesh.text = "";
        }
    }
}
