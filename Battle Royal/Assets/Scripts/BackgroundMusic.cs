using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    private static BackgroundMusic instance = null;

	public static BackgroundMusic Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        string sceneName = Application.loadedLevelName;

        Debug.Log(sceneName);
        if (sceneName == "test" || sceneName == "scene3" || sceneName == "scene5")
        {
            Destroy(this.gameObject);
            return;
        }

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
