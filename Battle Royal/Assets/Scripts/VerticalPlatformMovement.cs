using UnityEngine;
using System.Collections;

public class VerticalPlatformMovement : MonoBehaviour {
    int top = 0;
    int bot = 0;
    float platformMovementSpeed = 0;
    public bool moveUpFirst = true;
    public float distanceToMove = 5;
	// Use this for initialization
	void Start () {
        if(moveUpFirst==false)
        top = (int)((distanceToMove - 2.5) / .05) + 100;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 up = new Vector3(0, (float)0.05, 0);
        Vector3 down = new Vector3(0, (float)-0.05, 0);
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
        else
        {
            bot = 0;
            top = 0;
        }
	
	}
}
