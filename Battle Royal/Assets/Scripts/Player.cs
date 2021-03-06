﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{

    public float speed = 10.00f;
    float buttonTimer = 0f;
    float DASHTIME = 0f;
	float swingTimer = 0f;
	float arrowTimer = 0f;
    float dashCooldownTimer = 0.0f;
    public float DASHCOOLDOWN = 3.0f;
    public float JUMPCOOLDOWN = 0.5f;
	public float SWINGCOOLDOWN = 0.5f;
	public float ARROWCOOLDOWN = 0.5f;
    bool facingRight = false;
	private Animator animator;
	private AudioSource audio;
	private AudioClip swing;
	private AudioClip arrowSound;
	private AudioClip hitSound;
	//private NetworkAnimator networkAnimator;

	public delegate void flipDelegate();

	[SyncEvent]
	public event flipDelegate EventFlip;

	[SyncVar]
	public int health;

    // Use this for initialization
    void Start()
    {
		health = 100;
		//networkAnimator = GetComponent<NetworkAnimator> ();
		//networkAnimator = gameObject.AddComponent<NetworkAnimator> ();
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource> ();
		swing = Resources.Load<AudioClip> ("Sounds/Sword_Swing_3_P");
		arrowSound = Resources.Load<AudioClip> ("Sounds/Arrow_Whoosh");
		hitSound = Resources.Load<AudioClip> ("Sounds/Sword_Hit");
		//networkAnimator.animator = animator;
		//for (int i = -1; i < animator.parameterCount; i++) {
			//networkAnimator.SetParameterAutoSend(i, true);
		//}
    }

    // Update is called once per frame
    void Update()
    {

		if (health <= 0) {
			//player is dead call respawn
			GameObject.Find("Game_Manager").GetComponent<GameManager>().playerWasKilled(this);
		}

        if (!GetComponent<NetworkIdentity>().isLocalPlayer)   //Is this player my player?
        {
            return;
        }

        checkTimers();

        if (Input.GetButtonDown("Horizontal") && buttonTimer > 0 && dashCooldownTimer <= 0)
        {
            //double dash code
            dash();
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            buttonTimer = 0.5f;
        }
        if (Input.GetButton("Horizontal"))
        {
            float axisInput = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * axisInput, GetComponent<Rigidbody2D>().velocity.y);
            if(axisInput > 0 && facingRight == false){
                Cmdflip();
                facingRight = true;
            }
			else if(axisInput < 0 && facingRight == true){
				Cmdflip();
				facingRight = false;
			}
        }
		if (Input.GetButtonDown ("Punch")) {
			//do a sick punch
			if(swingTimer <= 0){
				swingTimer = SWINGCOOLDOWN;
				CmdPunch();
			}
		}
		if (Input.GetButtonDown ("Shoot")) {
			if(arrowTimer <= 0){
				arrowTimer = ARROWCOOLDOWN;
				CmdShoot();
			}
		}
    }

    void OnCollisionStay2D(Collision2D theCollision)
    {
        if (!GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }

        if ((theCollision.gameObject.tag == "Floor" && GetComponent<Rigidbody2D>().velocity.y < 10) && (Input.GetKey(KeyCode.Space)))
        {
			jump();
        }
    }

	public void takeDamage(int damage){

		audio.PlayOneShot(hitSound, 1);
		if (!isServer) {
			return;
		}

		health -= damage;

	}
	

	void jump(){
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 450);
	}

    void dash()
    {
        speed = 50.0f;
        DASHTIME = 0.3f;
        dashCooldownTimer = DASHCOOLDOWN;
    }

    void checkTimers()
    {
        if (buttonTimer > 0)
        {
            buttonTimer -= Time.deltaTime;
        }
        if (DASHTIME > 0)
        {
            DASHTIME -= Time.deltaTime;
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (DASHTIME <= 0)
        {
            speed = 10.00f;
        }
		if(swingTimer > 0 ){
			swingTimer -= Time.deltaTime;
		}
		if (arrowTimer > 0) {
			arrowTimer -= Time.deltaTime;
		}
    }
    [Command]
    void Cmdflip(){
        //transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
		EventFlip();
    }

	[Command]
	void CmdPunch(){
		//GameObject hitBox = (GameObject)Instantiate(Resources.Load("Prefabs/Hitbox_Punch"), transform.position, transform.rotation);
		if (isServer) {
			RpcPunch();
		}
		GameObject hitBox = (GameObject)Instantiate(Resources.Load("Prefabs/Hitbox_Punch"), new Vector2((transform.position.x - (2 * transform.localScale.x / 10)), transform.position.y), transform.rotation);
		hitBox.transform.parent = gameObject.transform;
		Destroy(hitBox, 1.0f);
		//NetworkServer.Spawn(hitBox);
	}

	[Command]
	void CmdShoot(){
		audio.PlayOneShot(arrowSound, 1);
		GameObject arrow = (GameObject)Instantiate(Resources.Load("Prefabs/Arrow"), new Vector2((transform.position.x - (3 * transform.localScale.x / 10)), transform.position.y), transform.rotation);
		Vector3 theScale = transform.localScale / 10;
		theScale.x *= -1;
		arrow.transform.localScale = theScale;
		arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(400 * transform.localScale.x),0));
		Destroy (arrow, 2.0f);
		NetworkServer.Spawn(arrow);
	}
	[ClientRpc]
	void RpcPunch(){
		audio.PlayOneShot(swing, 1);
		animator.SetTrigger("punch");
	}
}
