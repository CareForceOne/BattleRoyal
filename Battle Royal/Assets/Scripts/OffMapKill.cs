using UnityEngine;
using System.Collections;

public class OffMapKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D player){
		Debug.Log ("What?");
		
		if (player.gameObject.tag == "Player"){
			player.gameObject.GetComponent<Player>().takeDamage(100);
		}
	}
}
