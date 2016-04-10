using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	public float damage = 100f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
