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

    // Temporary solutions, i don't want to leave this here
    public Dictionary<string, int> maps = new Dictionary<string, int>();
    
    NetworkManager manager;

    void Start()
    {
        maps.Add("test", 1);
        maps.Add("scene3", 2);
        maps.Add("scene5", 3);
    }

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartMatchMaker();
        manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
    }

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

            manager.matchMaker.CreateMatch(ServerName.text, System.Convert.ToUInt32(MaxPlayers.captionText.text), true, Password.text, manager.OnMatchCreate);
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
