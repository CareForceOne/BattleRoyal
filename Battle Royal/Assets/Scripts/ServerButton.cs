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

    private NetworkMatch networkMatch;
    private NetworkID netID;
    private string pass;
    private int levelID;

    public void setServer(NetworkMatch networkMatch, NetworkID netID, int levelID, string password = "")
    {
        this.networkMatch = networkMatch;
        this.netID = netID;
        this.pass = password;
        this.levelID = levelID;
    }

    public void joinServer()
    {
        networkMatch.JoinMatch(netID, pass, callback);
    }

    private void callback(JoinMatchResponse response)
    {
        Debug.Log(response.success);
        Application.LoadLevel(levelID);
    }
}
