using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

    // Use this for initialization
    Collider2D ownerCollider;
    MeleeSystem ownerMelee;
    int opponentLayer;


	void Start () {
        ownerCollider = (Collider2D) GetComponentInParent(typeof(Collider2D));
        ownerMelee = (MeleeSystem)GetComponentInParent(typeof(MeleeSystem));
        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int playerLayer = LayerMask.NameToLayer("Player");

        //wenn wir spieler sind, sind gegner gegner, ansonsten is der spieler der gegner
        if (ownerCollider.gameObject.layer == enemyLayer)
            opponentLayer = playerLayer;
        else
            opponentLayer = enemyLayer;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == opponentLayer)
            ownerMelee.enemyHit(other.gameObject);
    }
}
