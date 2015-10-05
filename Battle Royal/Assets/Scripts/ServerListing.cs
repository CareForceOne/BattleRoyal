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
    public bool hasPassword;       //Does the server have a password?
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

	void Start () {	
		// Test data
		//testData.AddFirst (new ServerSettings ("JOHN", 3, 6, "abc"));

		UpdateListing();
	}

    void UpdateListing() {
        foreach(ServerSettings SS in serverList)
        {
            GameObject newButton = Instantiate(sampleButton) as GameObject;
            ServerButton newServerButton = newButton.GetComponent<ServerButton>();
            newServerButton.Name.text = SS.HostName;
            newServerButton.Mode.text = SS.Mode;
            newServerButton.Players.text = SS.curPlayers + " / " + SS.maxPlayers;
            newServerButton.hasPassword.text = SS.hasPassword ? "Yes" : "No";
            newServerButton.Ping.text = SS.ping.ToString();

            newServerButton.transform.SetParent(contentPanel);
        }
    }
}
