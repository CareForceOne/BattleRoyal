using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

enum Gamemodes
{
    TeamDeathmatch=1,
    FreeForAll=2,
    KingOfTheHill=3
};

public class HostGame : MonoBehaviour {
    public Text ServerName;
    public Dropdown Gamemode;
    public Text Password;
    public Dropdown MaxPlayers;
    public Dropdown Map;

    // Temporary solutions, i don't want to leave this here
    public Dictionary<string, int> accounts = new Dictionary<string, int>();

    void Start()
    {
        accounts.Add("test", 1);
        accounts.Add("scene3", 2);
        accounts.Add("scene5", 3);

        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(32, 25000, useNat);
    }

    void OnServerInitialized()
    {
        //MasterServer.RegisterHost("MyGameVer1.0.0_42", "My Game Instance", "This is a comment and place to store data");
        MasterServer.RegisterHost("test", "Temp", "Screw descriptions");
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            MasterServer.RequestHostList("test");
            Debug.Log("Server registered");
        }
            

        if (msEvent == MasterServerEvent.HostListReceived)
        {
            Debug.Log(MasterServer.PollHostList().Length);
        }

    }

    /*void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Debug.Log("Could not connect to master server: " + info);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");

    }*/

    public void Host()
    {
        RemoveErrors();
        if(true || isValid())
        {
            int level = -1;
            accounts.TryGetValue(Map.captionText.text, out level);

            // If we didn't get anything back we should probably handle that
            if (level == -1)
            { }

            // You're going to be the dedicated server for now
            
            
            Application.LoadLevel(level);
            //Debug.Log(MasterServer.PollHostList().Length);
        }
        else
        {
            DisplayErrors();
            Debug.Log("Unable to host with given input");
        }
    }

    // Make sure all of the public variables are set to something usable
	private bool isValid()
    {
        // Make sure a server name was given
        // Might also need to make sure the name isn't already in use
        if (ServerName.text.Length < 1)
            return false; 

        // Make sure the gamemode chosen exists
        if (Gamemode.value < 1 || Gamemode.value > System.Enum.GetNames(typeof(Gamemodes)).Length)
            return false;

        // A password doesn't have to be specified
        // If one was given we'll use it while hosting

        // Make sure the max players is set within the bounds
        // TODO use varaibles for this
        if (MaxPlayers.value < 2 || MaxPlayers.value > 8)
            return false;

        return true;
    }

    private void DisplayErrors()
    {

    }

    private void RemoveErrors()
    {

    }
}
