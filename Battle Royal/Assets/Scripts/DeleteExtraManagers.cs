using UnityEngine;
using System.Collections;

public class DeleteExtraManagers : MonoBehaviour {

	void Awake()
    {
        GameObject manager = GameObject.Find("NetworkManager");

        if(manager != null)
        {
            Destroy(manager.gameObject);
        }
    }
}
