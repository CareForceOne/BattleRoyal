using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {

	public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = "One sec";
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0) //The "Host" scene
        {
            //SetupMenuSceneButtons();
            StartCoroutine(SetupMenuSceneButtons());
        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    IEnumerator SetupMenuSceneButtons()
    {
        yield return new WaitForSeconds(0.3f);

        /*GameObject.Find().GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find().GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find().GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find().GetComponent<Button>().onClick.AddListener(JoinGame);*/
    }

    void SetupOtherSceneButtons()
    {
        //GameObject.Find().GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find().GetComponent<Button>().onClick.AQddListener(NetworkManager.singleton.StopHost);
    }
}
