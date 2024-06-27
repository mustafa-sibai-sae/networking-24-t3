using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button connectButton;

    void OnDestroy()
    {
        NetworkManager.instance.ConnectedToServerEvent -= OnConnectedToServer;
    }

    void Start()
    {
        NetworkManager.instance.ConnectedToServerEvent += OnConnectedToServer;

        connectButton.onClick.AddListener(() =>
        {
            NetworkManager.instance.ConnectToServer(playerNameInputField.text);
        });
    }

    void OnConnectedToServer()
    {
        SceneManager.LoadScene("Game");
    }
}