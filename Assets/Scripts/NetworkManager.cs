using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkManager : NetworkEvents
{
    Socket socket;

    public static NetworkManager instance;

    public PlayerData PlayerInfo { get; private set; }

    List<NetworkComponent> instansiatedGameObjects = new List<NetworkComponent>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        socket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
        }

        if (socket.Available > 0)
        {
            try
            {
                print($"Buffer size {socket.Available}");
                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);

                int bufferOffset = 0;
                int currentBufferSize = buffer.Length;

                while (currentBufferSize > 0)
                {
                    BasePacket bs = new BasePacket().Deserialize(buffer, bufferOffset);
                    currentBufferSize -= bs.PacketSize;

                    switch (bs.Type)
                    {
                        case BasePacket.PacketType.PositionPacket:
                            PositionPacket ps = new PositionPacket().Deserialize(buffer, bufferOffset);
                            bufferOffset += ps.PacketSize;
                            print("DODO!!!");
                            
                            ReceivePlayerPositionPacketEvent(ps.Position, ps.GameObjectID);
                            break;

                        case BasePacket.PacketType.InstantiatePacket:
                            InstantiatePacket ip = new InstantiatePacket().Deserialize(buffer, bufferOffset);
                            bufferOffset += ip.PacketSize;

                            GameObject go = Instantiate(Resources.Load<GameObject>(ip.PrefabName), ip.Position, ip.Rotation);

                            NetworkComponent nc = go.GetComponent<NetworkComponent>();
                            nc.OwnerID = ip.PlayerData.ID;
                            instansiatedGameObjects.Add(nc);
                            break;

                        case BasePacket.PacketType.DestroyPacket:
                            DestroyPacket dp = new DestroyPacket().Deserialize(buffer, bufferOffset);
                            bufferOffset += dp.PacketSize;

                            for (int i = 0; i < instansiatedGameObjects.Count; i++)
                            {
                                if (instansiatedGameObjects[i].GameObjectID == dp.GameObjectID)
                                {
                                    Destroy(instansiatedGameObjects[i].gameObject);
                                    instansiatedGameObjects.RemoveAt(i);
                                    break;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {

            }
        }
    }

    public void ConnectToServer(string playerName)
    {
        PlayerInfo = new PlayerData(Guid.NewGuid().ToString(), playerName);
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        socket.Blocking = false;

        ConnectedToServerEvent();
    }

    public void Send(byte[] buffer)
    {
        socket.Send(buffer);
    }
}