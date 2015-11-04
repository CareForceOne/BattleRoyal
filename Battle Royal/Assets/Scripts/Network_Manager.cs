using UnityEngine;
using System.Collections;

public class Network_Manager : MonoBehaviour {

    public string IP = "127.0.0.1";
    public int port = 25001;

    void OnGUI() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            if (GUI.Button(new Rect(100, 100, 100, 25), "Start client"))  {
                Network.Connect(IP, port);
            }
            if (GUI.Button(new Rect(100, 125, 100, 25), "Start Server"))
            {
                bool useNat = !Network.HavePublicAddress();
                Network.InitializeServer(10, port, useNat);
                
                //Debug.Log("server started");
            }
        }
        else {
            if (Network.peerType == NetworkPeerType.Client)  {
                GUI.Label(new Rect(100, 100, 100, 25), "Client");

                if (GUI.Button(new Rect(100, 125, 100, 25), "Logout")){
                    Network.Disconnect(250);
                }
            }
            if (Network.peerType == NetworkPeerType.Server){
                GUI.Label(new Rect(100, 100, 100, 25), "Server");
                GUI.Label(new Rect(100,125,100,25), "Connections" + Network.connections.Length);
                if(GUI.Button(new Rect(100,150,100,25), "logout")){
                    Network.Disconnect(250);
                }
            }
        }
    }

    void OnSeverInitilized()
    {
        Debug.Log("asdf");
    }
}
