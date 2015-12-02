using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
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

    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartMatchMaker();
        manager.SetMatchHost("mm.unet.unity3d.com", 443, true);

        manager.matchMaker.ListMatches(0, 20, "", UpdateListing);
    }

    private void UpdateListing(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success)
        {
            List<MatchDesc> servers = matchListResponse.matches;
            foreach (MatchDesc desc in servers)
            {
                AddServerButton(desc.name, "None", desc.currentSize, desc.maxSize, desc.isPrivate, 0, desc.networkId);
            }
        }
    }

    // Don't think i can have ping on here
    void AddServerButton(string HostName, string Gamemode, int curPlayers, int maxPlayers, bool hasPassword, int ping, NetworkID netID)
    {
        GameObject newButton = Instantiate(sampleButton) as GameObject;
        ServerButton newServerButton = newButton.GetComponent<ServerButton>();
        newServerButton.Name.text = HostName;
        newServerButton.Mode.text = Gamemode;
        newServerButton.Players.text = curPlayers + " / " + maxPlayers;
        newServerButton.hasPassword.text = hasPassword ? "Yes" : "No";
        newServerButton.Ping.text = ping.ToString();

        newServerButton.transform.SetParent(contentPanel);
        newServerButton.setServer(manager, netID);
    }
}
