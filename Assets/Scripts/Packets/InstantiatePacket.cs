using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePacket : BasePacket
{
    public string PrefabName { get; private set; }
    public Vector3 Position { get; private set; }
    public Quaternion Rotation { get; private set; }

    public InstantiatePacket()
    {
        PrefabName = "";
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
    }

    public InstantiatePacket(string prefabName, Vector3 position, Quaternion rotation, PlayerData playerData) :
        base(playerData, PacketType.InstantiatePacket)
    {
        PrefabName = prefabName;
        Position = position;
        Rotation = rotation;
    }

    public byte[] Serialize()
    {
        BeginSerialize();

        bw.Write(PrefabName);

        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Position.z);

        bw.Write(Rotation.x);
        bw.Write(Rotation.y);
        bw.Write(Rotation.z);
        bw.Write(Rotation.w);

        return EndSerialize();
    }

    public new InstantiatePacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        PrefabName = br.ReadString();

        Position = new Vector3(
            br.ReadSingle(),
            br.ReadSingle(),
            br.ReadSingle());

        Rotation = new Quaternion(
            br.ReadSingle(),
            br.ReadSingle(),
            br.ReadSingle(),
            br.ReadSingle());

        return this;
    }
}
