using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ServerListing : MonoBehaviour {

	private GameObject serversPanel;
	private RectTransform serversPanelTrans;

	private LinkedList<ServerSettings> testData = new LinkedList<ServerSettings>();

	// Use this for initialization
	void Start () {
		serversPanel = GameObject.Find("ServersPanel");
		serversPanelTrans = serversPanel.transform.GetComponent<RectTransform>();

		// Test data
		testData.AddFirst (new ServerSettings ("JOHN", 3, 6, "abc"));
		testData.AddFirst (new ServerSettings ("CENA", 9, 12, ""));
		testData.AddFirst (new ServerSettings ("John", 15, 18, "bad"));
		testData.AddFirst (new ServerSettings ("Doe", 21, 24, ""));
		testData.AddFirst (new ServerSettings ("Bleh", 27, 30, "password"));

		UpdateListing(testData);
	}

	void UpdateListing(LinkedList<ServerSettings> serverData) {
		float startHeight = serversPanelTrans.sizeDelta.y;
		//startHeight += 10; // Account for HeaderImage
		Debug.Log (startHeight);

		for (int i = 0; i < serverData.Count; i++) {
			ServerSettings data = serverData.First.Value;
			serverData.RemoveFirst();

			// Copy the host header, reuse it to list data
			GameObject hostHeader = GameObject.Find("HostHeader");
			GameObject hostData = Instantiate(hostHeader);
			hostData.name = "hostText" + (i + 1);
			RectTransform hostTrans = hostData.GetComponent<RectTransform>();

			hostTrans.SetParent(serversPanelTrans, false);
			hostTrans.localPosition = new Vector3(hostTrans.localPosition.x, startHeight);
			hostTrans.GetComponent<Text>().text = data.Host;

			// Adds the host object to the list
			/*GameObject hostGO = new GameObject();
			hostGO.name = "hostText" + (i + 1);

			RectTransform hostHeaderTrans = GameObject.Find("HostHeader").transform.GetComponent<RectTransform>();
			RectTransform hostTrans = hostGO.AddComponent<RectTransform>();
			hostTrans.parent = serversPanelTrans;
			hostTrans.anchoredPosition = hostHeaderTrans.anchoredPosition;
			hostTrans.sizeDelta = hostHeaderTrans.sizeDelta;
			hostTrans.eulerAngles = hostHeaderTrans.eulerAngles;

			Text t = hostGO.AddComponent<Text>();
			t.font = Font.CreateDynamicFontFromOSFont("Arial", 30);
			t.alignment = TextAnchor.MiddleLeft;
			t.horizontalOverflow = HorizontalWrapMode.Overflow;
			t.verticalOverflow = VerticalWrapMode.Overflow;
			t.text = data.Host;*/
			



			startHeight += 50;
		}
	}
	
	// Update is called once per frame
	void Update () {
		serversPanelTrans.sizeDelta = new Vector2(Screen.width, Screen.height - 130); // Account for title

	}
}
