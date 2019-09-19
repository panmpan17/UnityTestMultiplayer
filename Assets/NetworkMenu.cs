using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class NetworkMenu : MonoBehaviour {
	private uint roomSize = 2;

	[SerializeField]
	private Text ipText, roomNameText;
	[SerializeField]
	private GameObject canvas, section1, section2;
	private NetworkManager networkMgr;

	void Awake() {
		networkMgr = GetComponent<NetworkManager>();
		section1.SetActive(true);
		section2.SetActive(false);
	}

	public void Connect() {
		NetworkClient client = networkMgr.StartClient();
		client.Connect(ipText.text, 7777);
		canvas.SetActive(false);
	}
	public void StartServer() {
		networkMgr.StartHost();
		canvas.SetActive(false);
	}
	public void OpenMatchMaking() {
		if (networkMgr.matchMaker == null) {
			networkMgr.StartMatchMaker();
		}

		section1.SetActive(false);
		section2.SetActive(true);
	}

	public void CreateRoom() {
		if (roomNameText.text != "") {
			// name, size, public, password, public client address, private client addres
			// eloScoreForMatch, requestDomain, callback
			networkMgr.matchMaker.CreateMatch(roomNameText.text, 2, true, "", "", "",
				0, 0, OnMatchCreate);
		}
	}
	public void AutoMatch() {
		networkMgr.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
	}

	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success) {
			NetworkManager.singleton.StartHost(matchInfo);
			canvas.SetActive(false);
		} else {

		}
	}
	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success) {
			for (int i = 0; i < matches.Count; i++) {
				if (matches[i].currentSize == matches[i].maxSize) continue;

				NetworkManager.singleton.matchMaker.JoinMatch(matches[0].networkId, "" , "", "", 0, 0, OnMatchJoined);
			}
		}
	}

	private void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			NetworkManager.singleton.StartClient(matchInfo);
			canvas.SetActive(false);
		}
	}
}
