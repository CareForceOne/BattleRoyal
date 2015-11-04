using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject spawnObject = null;

    bool spawnedPlayer;//make a class that gets the value of if a spawned player = true.
    public void setSpawnedPlayer()
    {
      spawnedPlayer = false;
    }

    public bool getSpawnPlayer()
    {
        return spawnedPlayer;
    }

    void OnServerInitialized()
    {
        spawnedPlayer = true;
        spawn(Network.player);
    }

    void OnPlayerConnected(NetworkPlayer connectedPlayer)
    {
        spawn(connectedPlayer);
        spawnedPlayer = true;
    }

    void spawn(NetworkPlayer connectedPlayer)
    {
        Network.Instantiate(spawnObject, transform.position, transform.rotation, 0);
        spawnedPlayer = true;
    }
}