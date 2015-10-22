using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int projectileLayer = LayerMask.NameToLayer("Projectiles");
        Physics2D.IgnoreLayerCollision(projectileLayer, projectileLayer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
