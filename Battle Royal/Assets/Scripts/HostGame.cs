using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    
    public void Host()
    {
        if(isValid())
        {

        }
        else
        {
            // Probably should print off an error message to the user
        }
    }

    // Make sure all of the public variables are set to something usable
	private bool isValid()
    {
        // Make sure a server name was given
        if (ServerName.text.Length < 1)
            return false; 

        // Make sure the gamemode chosen exists
        if (Gamemode.value < 1 || Gamemode.value > System.Enum.GetNames(typeof(Gamemodes)).Length)
            return false;

        // A password doesn't have to be specified, so no check for that
        //if (Password.text)

        // Make sure the max players is set within the bounds
        // (Might want to allow 1 player for debugging)
        if (MaxPlayers.value < 2 || MaxPlayers.value > 8)
            return false;

        return true;
    }
}
