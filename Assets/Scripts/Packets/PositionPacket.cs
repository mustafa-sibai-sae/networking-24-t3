using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPacket : BasePacket
{
    public string GameObjectID { get; private set; }
    public Vector3 Position { get; private set; }

    public PositionPacket()
    {
        Position = Vector3.zero;
    }

    public PositionPacket(Vector3 position, string gameObjectID, PlayerData playerData) :
        base(playerData, PacketType.PositionPacket)
    {
        Position = position;
        GameObjectID = gameObjectID;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(GameObjectID);

        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Position.z);

        return EndSerialize();
    }

    public new PositionPacket Deserialize(byte[] buffer, int bufferOffset)
    {
        base.Deserialize(buffer, bufferOffset);

        GameObjectID = br.ReadString();

        Position = new Vector3(
            br.ReadSingle(),
            br.ReadSingle(),
            br.ReadSingle());

        return this;
    }
}
