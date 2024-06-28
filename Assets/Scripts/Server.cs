using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;

public class Server : MonoBehaviour
{
    Socket socket;
    List<Socket> clients = new List<Socket>();

    void Start()
    {
        socket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        socket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        socket.Listen(10);
        socket.Blocking = false;

        print("Waiting for client to connect...");
    }

    void Update()
    {
        try
        {
            clients.Add(socket.Accept());
            print("Accepted connection...");
        }
        catch
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < clients.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    clients[i].Send(new PositionPacket(new Vector3(1, 2, j), "dodo", new PlayerData("SERVER", "SERVER")).Serialize());
                }
            }
        }

        /*for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i].Available > 0)
            {
                try
                {
                    byte[] buffer = new byte[clients[i].Available];
                    clients[i].Receive(buffer);

                    for (int j = 0; j < clients.Count; j++)
                    {
                        if (i == j)
                            continue;

                        clients[j].Send(buffer);
                    }
                }
                catch
                {

                }
            }
        }*/
    }
}