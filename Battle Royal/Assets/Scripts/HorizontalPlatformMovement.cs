using UnityEngine;
using System.Collections;

public class HorizontalPlatformMovement : MonoBehaviour {

    int right = 0;
    int left = 0;
    public bool moveRightFirst = true;
    float horizontalPlatformSpeed = 0;
    public float distanceToMove = 5;


// Use this for initialization
void Start()
    {
        if (moveRightFirst == false)
        {
            right = (int)((distanceToMove - 2.5) / .05) + 100;
        }
    }
    
// Update is called once per frame
void Update()
    {
       
        Vector3 up = new Vector3(0, (float)0.05, 0);
        Vector3 down = new Vector3(0, (float)-0.05, 0);
        
        if (right < 50)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            right++;
            horizontalPlatformSpeed += (float).001;
            
        }
        else if (right < ((distanceToMove-2.5)/.05)+50)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            right++;
            
        }
        else if (right < ((distanceToMove - 2.5) / .05) + 100)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            right++;
            horizontalPlatformSpeed += (float)-.001;
        }
        else if (left < 50)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            left++;
            horizontalPlatformSpeed += (float)-.001;
        }
        else if (left < ((distanceToMove-2.5)/.05)+50)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            left++;
        }
        else if (left < ((distanceToMove - 2.5) / .05) + 100)
        {
            transform.Translate(horizontalPlatformSpeed, 0, 0);
            left++;
            horizontalPlatformSpeed += (float).001;
        }
        else
        {
            left = 0;
            right = 0;
        }

    }
}
