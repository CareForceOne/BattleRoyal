using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {
	
	GameObject killBlock;
	// Use this for initialization
	void Start () {
		killBlock = GameObject.Find ("KillBlock");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionEnter2D (Collision2D player){
		Debug.Log("even work?");

		if (player.gameObject.tag == "Player") {
			//gameObject.Find("KillBlock").SendMessage("kill");
			killBlock.SendMessage("killP");
			Debug.Log("Should ");

		}
		//kill = false;
	}
}
