using UnityEngine;
using System.Collections;

//Dient zum verwalten der Projektile

public class ProjectilePoolingSystem : MonoBehaviour {

	
	GameObject[] projectiles;
	public int pointer;
	public int projectileAmount = 4;
	//BoxCollider2D procoll = new BoxCollider2D();
	//SpriteRenderer prorender = new SpriteRenderer();
	//Rigidbody2D prorigid = new Rigidbody2D();

	// Use this for initialization
	void Awake() {
		//Erstellen des Projektilpools
		pointer = projectileAmount - 1;
		projectiles = new GameObject[projectileAmount];
		for (int i = 0; i < projectileAmount; ++i) {
			GameObject bullet = new GameObject();
			bullet.tag = "Projectile";
			bullet.layer = LayerMask.NameToLayer("Projectiles");
			bullet.AddComponent<BoxCollider2D>();
			BoxCollider2D collider =(BoxCollider2D) bullet.GetComponent(typeof(BoxCollider2D));
			collider.enabled = false;
			collider.size = new Vector2(0.65f,0.2f);
			bullet.AddComponent<SpriteRenderer>();
			Rigidbody2D rigid = bullet.AddComponent<Rigidbody2D>();
			rigid.gravityScale = 0;
			rigid.fixedAngle = true;
			bullet.AddComponent<Projectile>();
			SpriteRenderer s = bullet.GetComponent<SpriteRenderer>();
			s.enabled = false;
			Sprite proj = Resources.Load<Sprite>("07_effectsAndProjectiles") as Sprite;
			s.sprite = proj;
			projectiles[i] = bullet;
		}

		/* for (int i = 0; i < 4; i++) {
			procoll = (BoxCollider2D) projectiles[i].GetComponent(typeof(BoxCollider2D));
			procoll.enabled = false;
			
			prorender = (SpriteRenderer) projectiles[i].GetComponent(typeof(SpriteRenderer));
			prorender.enabled = false;
		} */



	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject parent = this.gameObject;
		if (pointer < 0 || pointer >= projectileAmount) {
			Debug.Log(parent.name);
		}
	}


	//Dient zum übergeben des obersten Projektils
	public GameObject getProjectile()
	{
		Debug.Log (this.gameObject.name + " -> Pointer:" + pointer);
		if (pointer >= 0) {
			BoxCollider2D temp = (BoxCollider2D)projectiles [pointer].GetComponent (typeof(BoxCollider2D));
			SpriteRenderer tempS = (SpriteRenderer)projectiles [pointer].GetComponent (typeof(SpriteRenderer));
			temp.enabled = true;
			tempS.enabled = true;
			return projectiles [pointer--];
		}
		return null;

		/*procoll = (BoxCollider2D)projectiles [pointer].GetComponent (typeof(BoxCollider2D));
		procoll.enabled = true;
			
		prorender = (SpriteRenderer) projectiles[pointer].GetComponent(typeof(SpriteRenderer));
		prorender.enabled = true;
			
		Debug.Log ("AuasgabrejGetBefore: " + pointer);
		Debug.Log ("GetAfter: " + pointer);
		return projectiles [pointer--];
		//Länge: 5 Pointer: 4 -> getProjectile() -> Länge: 5 Pointer: 3 -> getProjectile() -> Pointer: 2
		// storeProjectile */
	}


	//Dient zum lagern von Getroffenen/Abgeprallten Projektilen
	public void storeProjectile(GameObject projectile)
	{
		if (pointer < projectileAmount) {
			BoxCollider2D temp = (BoxCollider2D)projectile.GetComponent (typeof(BoxCollider2D));
			SpriteRenderer tempS = (SpriteRenderer)projectile.GetComponent (typeof(SpriteRenderer));
			temp.enabled = false;
			tempS.enabled = false;
			Rigidbody2D prorigid = (Rigidbody2D)projectile.GetComponent (typeof(Rigidbody2D));
			prorigid.velocity = new Vector2 (0, 0);
			pointer++;
		};

		/*procoll = (BoxCollider2D) projectile.GetComponent(typeof(BoxCollider2D));
		procoll.enabled = false;

		prorender = (SpriteRenderer) projectile.GetComponent(typeof(SpriteRenderer));
		prorender.enabled = false;

		prorigid = (Rigidbody2D) projectile.GetComponent(typeof(Rigidbody2D));
		prorigid.velocity = new Vector2(0, 0);

		Debug.Log ("StoreBfore: " + pointer);
		pointer++;
		Debug.Log ("StoreAfter: " + pointer);

		projectiles [pointer] = projectile; */
	}

}
