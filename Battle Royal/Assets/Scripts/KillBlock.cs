using UnityEngine;
using System.Collections;

public class KillBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D player, Collider2D wall, Collider wall2){
		Debug.Log("Does this even work?");
		if (player.gameObject.tag == "Player" && wall.gameObject.tag == "Crush" && wall2.gameObject.tag == "Crush") {
			Debug.Log("Should be what it hits");
			player.gameObject.GetComponent<Player>().takeDamage(100);
		}
	}
}
