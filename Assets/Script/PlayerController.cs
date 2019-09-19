using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	[SyncVar]
	private int score;

	[SerializeField]
	private GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		score = 0;
	}

	public void AddScore() { score += 1; }
	
	// [Command]
	[Client]
	public void Shoot(Vector3 shootFrom, Vector3 aim) {
		if (!isLocalPlayer) return;
		CmdOnShoot(shootFrom, aim);
	}

	[Command]
	void CmdOnShoot(Vector3 shootFrom, Vector3 aim) {
		RpcSpawnBullet(shootFrom, aim);
	}

	[ClientRpc]
	void RpcSpawnBullet(Vector3 shootFrom, Vector3 aim) {
		GameObject bulletObj = Instantiate(bulletPrefab);
		Bullet bullet = bulletObj.GetComponent<Bullet>();
		bullet.Init(gameObject, shootFrom, aim);
	}

	[Command]
	public void CmdGotShotBy(string shooterId) {
		PlayerController player = GameObject.Find(shooterId).GetComponent<PlayerController>();
		player.AddScore();
	}
}
