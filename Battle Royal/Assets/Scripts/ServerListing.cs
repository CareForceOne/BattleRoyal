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
    public string HostName;     //Friendly Name
    public int curPlayers;      //Players currently in the game
    public int maxPlayers;      //Max players the game can support
	
	public ServerSettings(string Host, string HostName, string Mode, int curPlayers, int maxPlayers, bool hasPassword, int ping) {
        this.HostName = HostName;
		this.curPlayers = curPlayers;
		this.maxPlayers = maxPlayers;
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
                AddServerButton(desc.name, desc.currentSize, desc.maxSize, desc.networkId);
            }
        }
    }

    // Don't think i can have ping on here
    void AddServerButton(string HostName, int curPlayers, int maxPlayers, NetworkID netID)
    {
        GameObject newButton = Instantiate(sampleButton) as GameObject;
        ServerButton newServerButton = newButton.GetComponent<ServerButton>();
        newServerButton.Name.text = HostName;
        newServerButton.Players.text = curPlayers + " / " + maxPlayers;
        newServerButton.transform.SetParent(contentPanel);
        newServerButton.setServer(manager, netID);
    }
}
