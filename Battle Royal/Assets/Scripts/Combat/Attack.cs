using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D player){
		//Debug.Log("trigger");
		if (player.gameObject.tag == "Player") {
			//Debug.Log("HIT");
			//AudioSource audio = GetComponent<AudioSource>();
			//audio.enabled = true;
			player.gameObject.GetComponent<Player>().takeDamage(20);
		}
	}
}
