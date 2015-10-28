using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Server : NetworkBehaviour {

	public Player player;
	
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
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//public void punch(){
		//flip ();
		//transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
	//}
}
