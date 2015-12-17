using UnityEngine;
using System.Collections;

    public class LevelManager : MonoBehaviour {

	    // Use this for initialization
	    void Start () {
            int projectileLayer = LayerMask.NameToLayer("Projectiles");
            Physics2D.IgnoreLayerCollision(projectileLayer, projectileLayer);
            int enemyLayer = LayerMask.NameToLayer("Enemies");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
