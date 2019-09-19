using UnityEngine;

public class Bullet : MonoBehaviour {
	private GameObject shooter;
	// private Vector3 vec;

	// Use this for initialization
	public void Init (GameObject _shooter, Vector3 shootFrom, Vector3 aim) {
		shooter = _shooter;
		transform.position = shootFrom;
		// vec = aim;
		GetComponent<Rigidbody2D>().velocity = aim;
	}
	
	// Update is called once per frame
	void Update () {
		// transform.Translate(vec * Time.deltaTime);

		if (Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 15) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == shooter) return;

		if (coll.gameObject.tag == PlayerSetup.LOCAL_PLAYER) {
			coll.gameObject.GetComponent<PlayerController>().CmdGotShotBy(shooter.name);
		}

		Destroy(gameObject);
	}
}
