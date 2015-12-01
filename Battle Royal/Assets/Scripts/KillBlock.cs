using UnityEngine;
using System.Collections;

public class KillBlock : MonoBehaviour {

	bool kill = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D player){
		Debug.Log("Does this even work?");
		if (player.gameObject.tag == "Crush") {
			Debug.Log("block detection?");
			kill = true;
		}
		if (player.gameObject.tag == "Player" && kill) {
			Debug.Log("Should be what it hits");
			player.gameObject.GetComponent<Player>().takeDamage(100);
		}
		//kill = false;
	}
}
