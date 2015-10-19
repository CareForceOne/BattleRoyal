using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Server : NetworkBehaviour {

	public Player_Movement player;
	
	// Use this for initialization
	void Start () {
		//if (NetworkClient.active) {
			player.EventFlip += flip;
			//player.EventPunch += punch;
		//}
	}

	// Update is called once per frame
	[ServerCallback]
	void Update () {
		
	}

	public void flip(){
		transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	}

	//public void punch(){
		//flip ();
		//transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	//}
}
