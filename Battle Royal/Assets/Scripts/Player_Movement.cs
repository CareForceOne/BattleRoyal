using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	float speed = 5.00f;
	float buttonTimer = 0f;
	float DASHTIME = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(buttonTimer > 0){
			buttonTimer -= Time.deltaTime; 
		}

		if (DASHTIME > 0) {
			DASHTIME -= Time.deltaTime;
		}
		if (Input.GetButtonDown ("Horizontal") && buttonTimer > 0) {
			//double dash code
			speed = 50.0f;
			DASHTIME = 0.3f;
			//Debug.Log("DASH");
		}
		if (DASHTIME <= 0) {
			speed = 5.00f;
		}

		if (Input.GetButtonDown("Horizontal")){
			buttonTimer = 0.5f;
		}

		if (Input.GetButton("Horizontal")) {
			//transform.Translate((Input.GetAxis("Horizontal") * Vector3.right) * Time.deltaTime * speed);

			GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Input.GetAxis("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);
			//Debug.Log (GetComponent<Rigidbody2D>().velocity);
		}

	}

	void OnCollisionStay2D(Collision2D theCollision){
        
		if (theCollision.gameObject.tag == "Floor" && buttonTimer == 0) {
			speed = 5.00f;
		}//this says if the player hits the floor and the dash is over, they will return to normal speed

		if (theCollision.gameObject.tag == "Floor" && (Input.GetKey(KeyCode.Space))) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * 450);
		}
	}
}
