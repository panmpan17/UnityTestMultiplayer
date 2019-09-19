using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerInputControl))]
public class PlayerSetup : NetworkBehaviour {
	public const string REMOTE_PLAYER = "RemotePlayer";
	public const string LOCAL_PLAYER = "LocalPlayer";

	private const string OBJECT_NAME = "player-";

	[SerializeField]
	Behaviour[] componentNeedDisabled;

	// Use this for initialization
	void Start () {
		gameObject.name = OBJECT_NAME + GetComponent<NetworkIdentity>().netId.ToString();

		if (!isLocalPlayer) {
			GetComponent<PlayerInputControl>().enabled = false;
			gameObject.tag = REMOTE_PLAYER;
			return;
		}

		gameObject.tag = LOCAL_PLAYER;
	}
}
