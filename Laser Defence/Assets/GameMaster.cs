using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {


	public GameObject PowerUpPrefab;
	public float PowerUpSpawnTime = 3.0f;
	private bool isSpawn = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Time.timeSinceLevelLoad);
		if (Time.timeSinceLevelLoad >= PowerUpSpawnTime && isSpawn == false) {
			Instantiate(PowerUpPrefab, this.transform.position, Quaternion.identity);
			isSpawn = true;
		}
	}
}
