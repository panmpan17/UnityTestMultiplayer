using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour {
	[HideInInspector]
	public bool isAtStartUp = true;
	NetworkClient myClient;

	public TextMesh text;

	// Update is called once per frame
	void Update () {
		if (isAtStartUp) {
			if (Input.GetKeyDown(KeyCode.S)) {
				NetworkServer.Listen(4444);
				isAtStartUp = false;

				text.text = "As Server";
			} else if (Input.GetKeyDown(KeyCode.C)) {
				myClient = new NetworkClient();
				myClient.RegisterHandler(MsgType.Connect, OnConnected);
				myClient.Connect("127.0.0.1", 4444);
				isAtStartUp = false;

				text.text = "As Client";
			} else if (Input.GetKeyDown(KeyCode.B)) {
				NetworkServer.Listen(4444);

				myClient = ClientScene.ConnectLocalServer();
				myClient.RegisterHandler(MsgType.Connect, OnConnected);
				isAtStartUp = false;

				text.text = "As Server & Client";
			}
		}
	}

	public void OnConnected(NetworkMessage netMsg) {
		text.text += " Connect to server";
	}
}
