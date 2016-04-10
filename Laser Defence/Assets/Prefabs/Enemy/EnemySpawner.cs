using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float enemySpawnDelay = 0.5f;

	float xmin;
	float xmax;

	private bool moveRight = true;

	// Use this for initialization
	void Start () {
		float Zdistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, Zdistance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, Zdistance));
		xmin = leftMost.x;
		xmax = rightMost.x;

		EnemySpawn ();
	}

	void EnemySpawn() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;			//Make the instantiate enemy inside the EnemyFormation(Optional)
		}
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", enemySpawnDelay);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height, 0));
	}


	// Update is called once per frame
	void Update () {
		//The position change and the judge process could be seperated!!!
		//There will be much more clearly!!!
		if (moveRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (transform.position.x + width / 2 >= xmax) {
				moveRight = false;
			}
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
			if(transform.position.x - width / 2 <= xmin) {
				moveRight = true;
			}
		}

		if (AllEnemyAreDead()) {
			Debug.Log("All enemy dead!");
			SpawnUntilFull();
		}
	}

	//To get the next free position to spawn enemy
	Transform NextFreePosition() {
		foreach (Transform child in transform) {
			if(child.childCount == 0){
				return child;
			}
		}
		return null;
	}

	//To check whether all the enemies are dead
	bool AllEnemyAreDead(){
		foreach (Transform child in transform) {
			if(child.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
