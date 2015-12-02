using UnityEngine;
using System.Collections;

public class KillBlock : MonoBehaviour {

	bool kill = false;
	bool kb = false;
	GameObject playr;
	// Use this for initialization
	void Start () {
	
	}

	IEnumerator toKill(){
		kill = true;
		yield return new WaitForSeconds(1);
		kill = false;
		
	}

	IEnumerator killBlockTouched()
	{
		kb = true;
		yield return new WaitForSeconds((float).5);
		kb = false;
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine (toKill ());
	}

	void killP()
	{
		if (kb) {
			playr.GetComponent<Player>().takeDamage (100);
		}
		else
			StartCoroutine (toKill ());
		/*
		Debug.Log ("Testing");
		wait ();
		if (kill) {
			Debug.Log("why don't you die?");
			player.GetComponent<Player> ().takeDamage (100);
		}*/
	}

	void OnTriggerEnter2D (Collider2D player){
	
		if (player.gameObject.tag == "Player" && kill){
			player.gameObject.GetComponent<Player>().takeDamage(100);
			//StartCoroutine (toKill ());
		}
		if (player.gameObject.tag == "Player") {
			playr = player.gameObject;
			StartCoroutine(killBlockTouched ());
		}
		//player.gameObject.GetComponent<Player>().takeDamage(100);
		//kill = false;
	}
}
