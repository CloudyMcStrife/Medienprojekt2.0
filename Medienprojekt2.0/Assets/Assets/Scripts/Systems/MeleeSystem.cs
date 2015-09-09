using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	GameObject owner;
	public Transform handPoint;
	Collider2D weaponCollider;
	GameObject weapon;
	SpriteRenderer s;

	// Use this for initialization
	void Awake () {
		owner = this.gameObject;
		weapon = new GameObject ();
		weapon.name = "Weapon";
		weapon.transform.position = handPoint.position;
		weapon.transform.rotation = new Quaternion (0f, 0f, 45f,0f);
		weapon.transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		weapon.transform.parent = owner.transform;
		s = weapon.AddComponent<SpriteRenderer> ();
		s.enabled = true;
		s.sortingLayerName = "EntitiesForeground";
		s.sortingOrder = 1;
		s.sprite = Resources.Load<Sprite>("02_items_5") as Sprite;
		weaponCollider = weapon.AddComponent<BoxCollider2D>();
		weaponCollider.enabled = true;
		weaponCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (s.sprite.ToString());
	}
}
