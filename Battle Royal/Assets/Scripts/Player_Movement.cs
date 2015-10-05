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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Input.GetAxis("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);
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
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 450);
        }
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
            speed = 5.00f;
        }
    }
}
