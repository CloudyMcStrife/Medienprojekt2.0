using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

	public float health;
	public float damage;
	float armor;
	float ammo;
	//int range gibt an ob fernkampf und wie weit die range des Spielers/Gegners ist. 0 = nahkampf, > 0 = fernkampf
	public int range;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getDamage()
	{
		return damage;
	}
	
	public void setHealth(float health)
	{
		this.health = health;
	}

	public float getHealth()
	{
		return health;
	}
}
