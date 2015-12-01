using UnityEngine;
using System.Collections;

public class KillBlockMovement : MonoBehaviour {
	
	int right = 0;
	int left = 0;
	int waitR = 0;
	int waitL = 0;
	int wait = 0;
	bool lF = false;
	public bool moveRightFirst = true;
	float horizontalPlatformSpeed = 0;
	public float distanceToMove = 5;
	public bool waitRight = false;
	public int waitRightFor = 0;
	public bool waitLeft = false;
	public int waitLeftFor = 0;
	public bool waitImmediatley = false;
	public int waitImmediatleyFor = 0;
	
	
	// Use this for initialization
	void Start()
	{
		if (moveRightFirst == false)
		{
			//right = (int)((distanceToMove - 2.5) / .05) + 100;
			lF  = true;
		}
		float b = 0;
		float c = 0;
		int a = 0;
		while(a < 50) {
			b = b + (float).002;
			c += b;
			a++;
		}
		Debug.Log (c);

	}
	
	// Update is called once per frame
	void Update()
	{

		if(waitImmediatley && wait < waitImmediatleyFor*30 && !lF)
		{
			wait++;
		}
		
		else if (right < 50 && !lF)
		{
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			right++;
			horizontalPlatformSpeed += (float).004;
			
		}
		else if (right < ((distanceToMove-2.5)/.05)+50 && !lF)
		{
			horizontalPlatformSpeed = (float).05;
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			right++;
			
		}
		else if (right < ((distanceToMove - 2.5) / .05) + 100 && !lF)
		{
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			right++;
			horizontalPlatformSpeed += (float)-.001;
		}
		else if(waitRight && waitR < waitRightFor*30 && !lF)
		{
			waitR++;
		}
		else if(waitImmediatley && wait < waitImmediatleyFor*30 && lF)
		{
			wait++;
		}
		else if (left < 50)
		{
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			left++;
			horizontalPlatformSpeed += (float)-.004;
		}
		else if (left < ((distanceToMove-2.5)/.05)+50)
		{
			horizontalPlatformSpeed = (float)-.05;
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			left++;
		}
		else if (left < ((distanceToMove - 2.5) / .05) + 100)
		{
			transform.Translate(horizontalPlatformSpeed, 0, 0);
			left++;
			horizontalPlatformSpeed += (float).001;
		}
		else if(waitLeft && waitL < waitLeftFor*30)
		{
			waitL++;
		}
		else
		{
			left = 0;
			right = 0;
			waitL = 0;
			waitR = 0;
			lF = false;
			waitImmediatley = false;
		}
		
	}
	
}
