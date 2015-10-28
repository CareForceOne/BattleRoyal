using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;

	public void playerWasKilled(Player player){
		NetworkConnection conn = player.connectionToClient;
		GameObject newPlayer = Instantiate<GameObject> (playerPrefab);
		Destroy (player.gameObject);

		NetworkServer.ReplacePlayerForConnection (conn, newPlayer, 0);
	}
}
