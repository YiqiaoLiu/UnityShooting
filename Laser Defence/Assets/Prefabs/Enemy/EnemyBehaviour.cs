using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float EnemyHealthyPoint = 500f;
	public GameObject EnemyBulletPrefab;
	public float EnemyBulletSpeed = -5f;
	public float EnemyFireRate = 1f;
	private float shotsPerSecond = 0.5f;
	public AudioClip enemyFireSound;
	public AudioClip enemyDestroy;
	private ScoreKeeper score;


	// Use this for initialization
	void Start () {
		score = GameObject.Find ("playerScore").GetComponent<ScoreKeeper>();
	}

	//The enemy's fire function
	void Fire(){
		GameObject EnemyBullet = Instantiate (EnemyBulletPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity) as GameObject;
		EnemyBullet.rigidbody2D.velocity = new Vector2 (0, EnemyBulletSpeed);
		AudioSource.PlayClipAtPoint (enemyFireSound, transform.position, 0.1f);
	}

	// Update is called once per frame
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){							//Using random to avoid the same pattern
			Fire();
		}
	}	

	void OnTriggerEnter2D(Collider2D col){
		BulletBehaviour bullet = col.gameObject.GetComponent<BulletBehaviour> ();
		if (bullet != null) {
			bullet.Hit();
			EnemyHealthyPoint -= bullet.damage;
			if(EnemyHealthyPoint <= 0) {
				score.playerScore += 100;
				AudioSource.PlayClipAtPoint(enemyDestroy, transform.position, 2f);
				Destroy(gameObject);
			}
		}
	}
}
