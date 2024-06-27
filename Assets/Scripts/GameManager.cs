using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(0, 10 + 1), 0, 0);

        GameObject go = Instantiate(Resources.Load<GameObject>("Player"), spawnPosition, Quaternion.identity);
        go.GetComponent<NetworkComponent>().OwnerID = NetworkManager.instance.PlayerInfo.ID;

        NetworkManager.instance.Send(new InstantiatePacket(
            "Player",
            spawnPosition,
            Quaternion.identity,
            NetworkManager.instance.PlayerInfo).Serialize());
    }

    void Update()
    {

    }
}
