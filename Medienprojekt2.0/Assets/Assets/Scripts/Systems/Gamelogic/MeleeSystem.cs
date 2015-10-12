using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public GameObject owner;

	//Blocking Variables
	public GameObject blockObj;
	public Transform meleeCheck;

	BoxCollider2D blockColl;
	SpriteRenderer spriteRen;
	AttributeComponent acplayer;
	CharacterMovement cmplayer;

	
	public GameObject weaponObj;

	// Use this for initialization
	void Awake () {
		if (blockObj != null) {
			blockColl = (BoxCollider2D)blockObj.GetComponent (typeof(BoxCollider2D));
			spriteRen = (SpriteRenderer)blockObj.GetComponent(typeof(SpriteRenderer));
			blockColl.enabled = false;
			spriteRen.enabled = false;
		}
		if (weaponObj != null) {
		}
		acplayer = (AttributeComponent)owner.GetComponent (typeof(AttributeComponent));
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void block(bool newValue)
	{
		blockColl.enabled = newValue;
		spriteRen.enabled = newValue;
	}

	public void playerMeleeAttack(bool facingRight)
	{
		Vector2 direction;

		if (facingRight)
			direction = new Vector2 (1, 0);
		else
			direction = new Vector2 (-1, 0);

		RaycastHit2D[] meleeCollider = Physics2D.BoxCastAll (meleeCheck.position, new Vector2(1, 0.5f), 0, direction, 0.1f);

		if (meleeCollider.Length != 0) {
			foreach (RaycastHit2D c in meleeCollider) {
				if (c.collider.gameObject.tag == "Enemy") {
					AttributeComponent enemyac = (AttributeComponent)c.collider.gameObject.GetComponent (typeof(AttributeComponent));
					enemyac.setHealth (enemyac.getHealth() - acplayer.getDamage());
					Debug.Log ("Meleehit");
				}
			}
		}
	}
}
