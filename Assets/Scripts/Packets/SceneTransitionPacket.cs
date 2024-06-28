using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPacket : BasePacket
{
    public string SceneName { get; private set; }

    public SceneTransitionPacket()
    {
        SceneName = "";
    }

    public SceneTransitionPacket(string sceneName, PlayerData playerData) :
        base(playerData, PacketType.SceneTransitionPacket)
    {
        SceneName = sceneName;
    }

    public byte[] Serialize()
    {
        BeginSerialize();
        bw.Write(SceneName);
        return EndSerialize();
    }

    public new SceneTransitionPacket Deserialize(byte[] buffer, int bufferOffset)
    {
        base.Deserialize(buffer, bufferOffset);

        SceneName = br.ReadString();

        return this;
    }
}
