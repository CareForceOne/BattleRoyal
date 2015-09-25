using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject spawnObject = null;

    void OnServerInitialized(){
        Debug.Log("spawn");
        spawn(Network.player);
        
    }

    void OnPlayerConnected(NetworkPlayer connectedPlayer){
        spawn(connectedPlayer);
    }

    void spawn(NetworkPlayer connectedPlayer) {
        Network.Instantiate(spawnObject, transform.position, transform.rotation,0);
    }
}
