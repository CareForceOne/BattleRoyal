using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleSound : MonoBehaviour {
    bool sound = true;

	public void toggleSound()
    {
        sound = !sound;

        // TODO
        // Still haven't figured out how to change the right
        // text object.
        Text text = gameObject.GetComponentInChildren<Text>();
        if(sound)
        {
            text.text = "Sound: On";
        }
        else
        {
            text.text = "Sound: Off";
        }
    }
}
