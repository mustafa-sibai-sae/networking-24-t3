using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponent : MonoBehaviour
{
    public string OwnerID;
    public string GameObjectID;

    void Start()
    {
        GameObjectID = Guid.NewGuid().ToString();

        Vector3 dodo = new Vector3(0, 1, 0);

        dodo /= 5.0f;

    }

    public bool IsMine()
    {
        return NetworkManager.instance.PlayerInfo.ID == OwnerID;
    }
}