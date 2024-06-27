using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPacket : BasePacket
{
    public string GameObjectID { get; private set; }

    public DestroyPacket()
    {
        GameObjectID = "";
    }

    public DestroyPacket(string gameObjectID, PlayerData playerData) :
        base(playerData, PacketType.DestroyPacket)
    {
        GameObjectID = gameObjectID;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(GameObjectID);

        return EndSerialize();
    }

    public new DestroyPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        GameObjectID = br.ReadString();

        return this;
    }
}
