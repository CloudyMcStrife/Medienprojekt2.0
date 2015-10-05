using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

    public float health;
	public float damage;
	float armor;
	int ammo;
    int ammoCap;
	//int range gibt an ob fernkampf und wie weit die range des Spielers/Gegners ist. 0 = nahkampf, > 0 = fernkampf
	public int range;

	// Use this for initialization
	void Start () {
        health = 100;
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

    public int getAmmo()
    {
        return ammo;
    }

    public void increaseAmmo(int value)
    {
        ammo += value;
    }

    public void decrementAmmo()
    {
        ammo--;
    }

    public int getAmmoCap()
    {
        return ammoCap;
    }

    public void increaseAmmoCap(int value)
    {
        ammoCap += value;
    }
}
