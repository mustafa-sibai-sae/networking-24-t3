using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    TMP_Text playerDataText;

    void Start()
    {
        playerDataText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        playerDataText.text = NetworkManager.instance.PlayerInfo.ID;
    }
}