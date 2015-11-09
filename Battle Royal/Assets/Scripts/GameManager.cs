using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject manager;
	//public Camera camera;

	public void playerWasKilled(Player player){
		//camera = Camera.main;
		//CameraTracking cameraScript = camera.GetComponent<CameraTracking> ();

		NetworkConnection conn = player.connectionToClient;
		Transform spawnPoint = manager.GetComponent<NetworkManager> ().GetStartPosition ();
		//GameObject newPlayer = Instantiate<GameObject> (playerPrefab);
		GameObject newPlayer = (GameObject) Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Destroy (player.gameObject);

		NetworkServer.ReplacePlayerForConnection (conn, newPlayer, 0);

	}
}
