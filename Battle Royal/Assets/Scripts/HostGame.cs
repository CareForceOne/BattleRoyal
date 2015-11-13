using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

enum Gamemodes
{
    TeamDeathmatch = 1,
    FreeForAll = 2,
    KingOfTheHill = 3
};

public class HostGame : MonoBehaviour
{
    public Text ServerName;
    public Dropdown Gamemode;
    public Text Password;
    public Dropdown MaxPlayers;
    public Dropdown Map;

    public Text debugText;

    // Temporary solutions, i don't want to leave this here
    public Dictionary<string, int> maps = new Dictionary<string, int>();

    NetworkMatch networkMatch;

    void Start()
    {
        maps.Add("test", 1);
        maps.Add("scene3", 2);
        maps.Add("scene5", 3);

        //bool useNat = !Network.HavePublicAddress();
        //Network.InitializeServer(32, 25000, useNat);

        GameObject debugTextObject = GameObject.Find("DebugText");
        debugText = debugTextObject.GetComponent<Text>();

        debugText.text = "debug";
    }

    int lastCount = 0;
    void OnGUI()
    {
        if (GUILayout.Button("List rooms"))
        {
            GUILayout.Label("Current rooms: " + lastCount);
            networkMatch.ListMatches(0, 20, "", OnList);
        }
    }

    public void OnList(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success)
        {
            lastCount = matchListResponse.matches.Count;
            debugText.text = "Player Count: " + matchListResponse.matches.Count;
            Debug.Log(matchListResponse.matches.Count);
        }

    }

    void Awake()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
    }

    // Old attempt, trying something new
    /*void OnServerInitialized()
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

    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Debug.Log("Could not connect to master server: " + info);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");

    }*/

    int levelID = 0;
    public void Host()
    {
        RemoveErrors();
        if (true || isValid())
        {
            maps.TryGetValue(Map.captionText.text, out levelID);

            // If we didn't get anything back we should probably handle that
            if (levelID == -1)
            { }

            CreateMatchRequest create = new CreateMatchRequest();
            create.name = ServerName.text;
            create.size = System.Convert.ToUInt32(MaxPlayers.captionText.text);
            create.advertise = true;
            create.password = Password.text;
            // Gamemode?

            networkMatch.CreateMatch(create, OnMatchCreate);

            //Application.LoadLevel(level);
        }
        else
        {
            DisplayErrors();
            Debug.Log("Unable to host with given input");
        }
    }

    public void OnMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse.success)
        {
            debugText.text = "Created Match";
            Utility.SetAccessTokenForNetwork(matchResponse.networkId, new NetworkAccessToken(matchResponse.accessTokenString));
            NetworkServer.Listen(new MatchInfo(matchResponse), 9000);

            networkMatch.ListMatches(0, 20, "", OnMatchList);
        }
        else
        {
            debugText.text = "Failed creating match";
            Debug.Log("Create match failed");
        }
    }



    public void OnMatchList(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success)
        {
            debugText.text = "Player count: " + matchListResponse.matches.Count;
            Debug.Log(matchListResponse.matches.Count);
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
