using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Projectiles"), LayerMask.NameToLayer ("Projectiles"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
