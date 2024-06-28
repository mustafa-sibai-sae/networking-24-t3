using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BasePacket
{
    public enum PacketType
    {
        None,
        PositionPacket,
        InstantiatePacket,
        SceneTransitionPacket,
        DestroyPacket
    }

    public PacketType Type { get; private set; }
    public PlayerData PlayerData { get; private set; }
    public int PacketSize { get; private set; }

    protected MemoryStream wms;
    protected BinaryWriter bw;

    protected MemoryStream rms;
    protected BinaryReader br;

    public BasePacket()
    {
        Type = PacketType.None;
        PlayerData = null;
    }

    public BasePacket(PlayerData playerData, PacketType type)
    {
        Type = type;
        PlayerData = playerData;
    }

    protected void BeginSerialize()
    {
        wms = new MemoryStream();
        bw = new BinaryWriter(wms);

        bw.Write((int)Type);
        bw.Write(PlayerData.ID);
        bw.Write(PlayerData.Name);
    }

    protected byte[] EndSerialize()
    {
        PacketSize = (int)wms.Length + 4;
        bw.Write(PacketSize);
        return wms.ToArray();
    }

    public BasePacket Deserialize(byte[] buffer, int bufferOffset)
    {
        rms = new MemoryStream(buffer);
        br = new BinaryReader(rms);
        rms.Seek(bufferOffset, SeekOrigin.Begin);

        Type = (PacketType)br.ReadInt32();
        PlayerData = new PlayerData(br.ReadString(), br.ReadString());

        return this;
    }
}
