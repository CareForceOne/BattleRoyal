using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleSound : MonoBehaviour {
    bool sound = true;

	public void toggleSound()
    {
        sound = !sound;

        Text text = gameObject.GetComponentInChildren<Text>();
        if(sound)
        {
            text.text = "Sound: On";
			BackgroundMusic.Instance.gameObject.SetActive(true);
        }
        else
        {
            text.text = "Sound: Off";
			BackgroundMusic.Instance.gameObject.SetActive(false);
        }
    }
}
