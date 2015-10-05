using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public GameObject owner;

	//Blocking Variables
	public GameObject blockObj;
	BoxCollider2D blockColl;
	SpriteRenderer spriteRen;

	
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
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void block(bool newValue)
	{
		blockColl.enabled = newValue;
		spriteRen.enabled = newValue;
	}
}
