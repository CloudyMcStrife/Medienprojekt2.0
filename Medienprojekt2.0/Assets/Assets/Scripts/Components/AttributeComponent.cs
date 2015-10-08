using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

    public float health;
	public float damage;
	float armor;
    //current ammo
	public int ammo;
    //max ammo player can carry
    public int ammoCap;
	//int range gibt an ob fernkampf und wie weit die range des Spielers/Gegners ist. 0 = nahkampf, > 0 = fernkampf
	public int range;

    static float cooldown1 = 1.0f;
    bool cooldown1Active = false;
    bool skill1cooldown = false;

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

    public bool getCooldown1Active()
    {
        return cooldown1Active;
    }

    public void setCooldown1Active(bool value)
    {
        cooldown1Active = value;
    }

    public float getCooldown1()
    {
        return cooldown1;
    }
}
