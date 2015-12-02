using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class HostGame : MonoBehaviour
{
    public Text ServerName;
    public Dropdown Gamemode;
    public Text Password;
    public Dropdown MaxPlayers;
    public Dropdown Map;
    
    NetworkManager manager;

    void Awake()
    {
        Dropdown mapDropdown = Map.GetComponent<Dropdown>();
        mapDropdown.options.Add(new Dropdown.OptionData("scene1"));
        mapDropdown.options.Add(new Dropdown.OptionData("scene2"));
        mapDropdown.options.Add(new Dropdown.OptionData("scene3"));
        mapDropdown.options.Add(new Dropdown.OptionData("scene4"));
        mapDropdown.options.Add(new Dropdown.OptionData("scene5"));

        manager = GetComponent<NetworkManager>();
        manager.StartMatchMaker();
        manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
    }
    
    public void Host()
    {
        if (isValid())
        {
            manager.onlineScene = Map.captionText.text;
            manager.matchMaker.CreateMatch(ServerName.text, System.Convert.ToUInt32(MaxPlayers.captionText.text), true, "", manager.OnMatchCreate);
        }
    }

    // Make sure all of the public variables are set to something usable
    private bool isValid()
    {
        // Make sure a server name was given
        if (ServerName.text.Length < 1)
            return false;

        // Make sure the max players is set within the bounds
        uint maxPlayers = System.Convert.ToUInt32(MaxPlayers.captionText.text);
        if (maxPlayers < 2 || maxPlayers > 8)
            return false;

        return true;
    }
}
