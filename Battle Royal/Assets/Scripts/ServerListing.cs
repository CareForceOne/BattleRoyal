using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ServerSettings {
    public string Host;         //IP
    public string HostName;     //Friendly Name
    public string Mode;         //Current game mode
    public int curPlayers;      //Players currently in the game
    public int maxPlayers;      //Max players the game can support
    public bool hasPassword;    //Does the server have a password?
    public int ping;
	
	public ServerSettings(string Host, string HostName, string Mode, int curPlayers, int maxPlayers, bool hasPassword, int ping) {
		this.Host = Host;
        this.HostName = HostName;
        this.Mode = Mode;
		this.curPlayers = curPlayers;
		this.maxPlayers = maxPlayers;
		this.hasPassword = hasPassword;
        this.ping = ping;
	}
}

public class ServerListing : MonoBehaviour {
	public GameObject sampleButton;
	public List<ServerSettings> serverList;

    public Transform contentPanel;

	void Start()
    {
        MasterServer.RequestHostList("test");
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");

        if (msEvent == MasterServerEvent.HostListReceived)
        {
            Debug.Log(MasterServer.PollHostList().Length);
        }
            //UpdateListing();

    }

    private void UpdateListing()
    {
        if (MasterServer.PollHostList().Length != 0)
        {
            HostData[] hostData = MasterServer.PollHostList();
            foreach (HostData HD in hostData)
            {
                AddServerButton(HD.gameName, HD.gameType, HD.connectedPlayers, HD.playerLimit, HD.passwordProtected, -1);
            }
            MasterServer.ClearHostList();
        }
    }

    void AddServerButton(string HostName, string Gamemode, int curPlayers, int maxPlayers, bool hasPassword, int ping)
    {
        GameObject newButton = Instantiate(sampleButton) as GameObject;
        ServerButton newServerButton = newButton.GetComponent<ServerButton>();
        newServerButton.Name.text = HostName;
        newServerButton.Mode.text = Gamemode;
        newServerButton.Players.text = curPlayers + " / " + maxPlayers;
        newServerButton.hasPassword.text = hasPassword ? "Yes" : "No";
        newServerButton.Ping.text = ping.ToString();

        newServerButton.transform.SetParent(contentPanel);
    }
}
