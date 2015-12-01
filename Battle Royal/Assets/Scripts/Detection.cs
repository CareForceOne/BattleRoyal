using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionEnter2D (Collision2D player){
		Debug.Log("even work?");

		if (player.gameObject.tag == "Player") {
			Debug.Log("Should ");

		}
		//kill = false;
	}
}
