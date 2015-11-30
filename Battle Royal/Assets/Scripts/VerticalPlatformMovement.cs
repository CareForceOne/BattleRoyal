using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class VerticalPlatformMovement : NetworkBehaviour {
    int top = 0;
    int bot = 0;
	int waitT = 0;
	int waitB = 0;
    float platformMovementSpeed = 0;
    public bool moveUpFirst = true;
    public float distanceToMove = 5;
	public bool waitTop = false;
	public int waitTopFor = 0;
	public bool waitBot = false;
	public int waitBotFor = 0;


	// Use this for initialization
	void Start () {
        if(moveUpFirst==false)
        top = (int)((distanceToMove - 2.5) / .05) + 100;
    }
	
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
       // Debug.Log(platformMovementSpeed);      
        if(top < 50)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            top++;
            platformMovementSpeed += (float).001;
        }
        else if (top< (int)((distanceToMove - 2.5) / .05) + 50)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            top++;
        }
        else if(top< (int)((distanceToMove - 2.5) / .05) + 100)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            top++;
            platformMovementSpeed += (float)-.001;
        }
		else if (waitTop && waitT<waitTopFor*30)
		{
			waitT++;
		}
        else if (bot < 50)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            bot++;
            platformMovementSpeed += (float)-.001;
        }
        else if(bot < (int)((distanceToMove - 2.5) / .05) + 50)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            bot++;
        }
        else if (bot < (int)((distanceToMove - 2.5) / .05) + 100)
        {
            transform.Translate(0, platformMovementSpeed, 0);
            bot++;
            platformMovementSpeed += (float).001;
        }
		else if (waitBot && waitB<waitBotFor*30)
		{
			waitB++;
		}
        else
        {
            bot = 0;
            top = 0;
			waitT = 0;
			waitB = 0;
        }
	
	}
}
