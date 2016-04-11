using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed = 5f;
	public float padding = 0.5f;	//the offset in the boundry
	public GameObject projectile;	//The bullet object
	public float laserSpeed = 5f;
	private float lastFireTime;
	public float fireRate = 1f;
	public float playerHealthyPoint = 250f;

	public AudioClip fireAudio;

	float xmin;
	float xmax;
	float ymin;
	float ymax;

	// Use this for initialization
	void Start () {
		float Zdistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, Zdistance));	//The left-most position
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, Zdistance));	//The right-most position
		Vector3 topMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, Zdistance));		//The top-most position
		Vector3 bottomMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, Zdistance));	//The bottom-most position
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		ymin = bottomMost.y + padding;
		ymax = topMost.y - padding;
	}

	//This is the function about the player's fire
	void Fire(){
		lastFireTime = Time.time;
		GameObject laser = Instantiate(projectile, transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		laser.rigidbody2D.velocity = new Vector2(0, laserSpeed);
		AudioSource.PlayClipAtPoint (fireAudio, transform.position);
	}

	// Update is called once per frame
	void Update () {

		//Update the player's position
		Vector3 playerPosition = this.transform.position;
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			playerPosition += Vector3.up * Time.deltaTime * speed;  
		}
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			playerPosition += Vector3.down * Time.deltaTime * speed;  
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			playerPosition += Vector3.left * Time.deltaTime * speed;  
		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			playerPosition += Vector3.right * Time.deltaTime * speed;  
		}

		//Instantiate the bullet object
		if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {
			if(Time.time - lastFireTime >= fireRate){
				Fire ();
			}
		}
		//Finally update the position of the player
		this.transform.position = new Vector3(Mathf.Clamp(playerPosition.x, xmin, xmax), Mathf.Clamp(playerPosition.y, ymin, ymax), playerPosition.z);	//Restriction of the player's position
	}

	void OnTriggerEnter2D(Collider2D col){
		BulletBehaviour bullet = col.gameObject.GetComponent<BulletBehaviour> ();
		if (bullet != null) {
			bullet.Hit();
			playerHealthyPoint -= bullet.damage;
			if(playerHealthyPoint <= 0){
				Destroy(gameObject);
			}
		}
	}
}
