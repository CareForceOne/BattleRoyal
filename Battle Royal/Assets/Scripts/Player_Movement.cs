using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_Movement : NetworkBehaviour
{

    public float speed = 5.00f;
    float buttonTimer = 0f;
    float DASHTIME = 0f;
    float dashCooldownTimer = 0.0f;
    public float DASHCOOLDOWN = 3.0f;
    public float JUMPCOOLDOWN = 0.5f;
    float jumpTimer = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NetworkIdentity>().isLocalPlayer)   //need to find out if I am controlling my own player
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
            if (Input.GetButtonDown("Horizontal") && buttonTimer > 0 && dashCooldownTimer <= 0)
            {
                //double dash code
                speed = 50.0f;
                DASHTIME = 0.3f;
                dashCooldownTimer = DASHCOOLDOWN;
                //Debug.Log("DASH");
            }
            if (DASHTIME <= 0)
            {
                speed = 5.00f;
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                buttonTimer = 0.5f;
            }

            if (Input.GetButton("Horizontal"))
            {
                //transform.Translate((Input.GetAxis("Horizontal") * Vector3.right) * Time.deltaTime * speed);

                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Input.GetAxis("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);
                //Debug.Log (GetComponent<Rigidbody2D>().velocity);
            }
        }

    }

    void OnCollisionStay2D(Collision2D theCollision)
    {

        if (theCollision.gameObject.tag == "Floor" && buttonTimer == 0)
        {
            speed = 5.00f;
        }//this says if the player hits the floor and the dash is over, they will return to normal speed

        if ((theCollision.gameObject.tag == "Floor" && GetComponent<Rigidbody2D>().velocity.y < 10) && (Input.GetKey(KeyCode.Space)))
        {
            //jumpTimer = JUMPCOOLDOWN;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 450);
        }

    }
}
