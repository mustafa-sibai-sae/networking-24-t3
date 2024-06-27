using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    NetworkComponent networkComponent;

    void OnDestroy()
    {
        NetworkManager.instance.ReceivePlayerPositionPacketEvent -= OnPositionReceived;
    }

    void Start()
    {
        NetworkManager.instance.ReceivePlayerPositionPacketEvent += OnPositionReceived;
        networkComponent = GetComponent<NetworkComponent>();
    }

    void Update()
    {
        if (networkComponent.IsMine())
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
                NetworkManager.instance.Send(new PositionPacket(
                    transform.position,
                    networkComponent.GameObjectID,
                    NetworkManager.instance.PlayerInfo).Serialize());
            }
        }
    }

    void OnPositionReceived(Vector3 position, string GameObjectID)
    {
        if (!networkComponent.IsMine() && GameObjectID == networkComponent.GameObjectID)
        {
            transform.position = position;
        }
    }
}