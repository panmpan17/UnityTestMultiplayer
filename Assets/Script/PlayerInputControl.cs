using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputControl : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private float bulletSpeed;

	private PlayerController data;
	private Rigidbody2D rigidbody2d;

	void Awake() {
		data = GetComponent<PlayerController>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		// if (horizontal != 0 || vertical != 0)
		rigidbody2d.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

		if (Input.GetMouseButtonDown(0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = transform.position.z;

			float vecX = pos.x - transform.position.x;
			float vecY = pos.y - transform.position.y;
			float mutiplier = Vector3.Distance(pos, transform.position) / bulletSpeed;

			data.Shoot(transform.position, new Vector3(vecX / mutiplier, vecY / mutiplier, 0));
		}
	}
}
