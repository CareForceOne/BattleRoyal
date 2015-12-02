using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class ServerButton : MonoBehaviour {
    public Button button;
    public Text Name;
    public Text Players;
    
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
        manager.matchMaker.JoinMatch(netID, "", manager.OnMatchJoined);
    }
}
