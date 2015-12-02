using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class Disconnect : MonoBehaviour {

    private NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            manager.StopHost();
        }
    }
}
