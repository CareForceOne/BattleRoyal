using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class ServerButton : MonoBehaviour {
    public Button button;
    public Text Name;
    public Text Mode;
    public Text Players;
    public Text hasPassword;
    public Text Ping;
    
    private NetworkManager manager;
    private NetworkID netID;
    private string password;

    public void setServer(NetworkManager manager, NetworkID netID, string password = "")
    {
        this.manager = manager;
        this.netID = netID;
        this.password = password;
    }

    public void joinServer()
    {
        manager.matchName = "";
        manager.matchSize = 3;
        manager.matchMaker.JoinMatch(netID, "", callback);
    }

    private void callback(JoinMatchResponse response)
    {
        if(response.success)
        {
            Debug.Log("What");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
