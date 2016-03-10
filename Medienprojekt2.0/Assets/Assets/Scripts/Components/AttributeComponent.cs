using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float staminaPerSecond = 0f;
	public float damage;
    public bool cloneAlive = false;
	float armor;
    //current ammo
	public int ammo;
    //max ammo player can carry
    public int ammoCap;
	//int range gibt an ob fernkampf und wie weit die range des Spielers/Gegners ist. 0 = nahkampf, > 0 = fernkampf
	public int range;

    //Cooldown für Plasma-Schuss
    static float cooldown1 = 1.0f;
    bool cooldown1Active = false;
    bool skill1cooldown = false;

    static float cooldown2 = 10.0f;
    static float ttl = 5.0f;
    float attl = ttl;
    bool cooldown2Active = false;
    bool skill2cooldown = false;

    MeleeSystem meleeSys;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        meleeSys = (MeleeSystem)GetComponent(typeof(MeleeSystem));
	}
	
	// Update is called once per frame
	void Update () {
        if (staminaPerSecond > 0.0f && stamina < maxStamina && !meleeSys.animationRunning)
        {
            stamina = Mathf.Clamp(stamina+staminaPerSecond * Time.deltaTime,0,maxStamina);
            Debug.Log(stamina);
        }   
	}

    void FixedUpdate()
    {
        if (attl > 0)
            attl -= Time.deltaTime;
        if (attl <= 0 && cloneAlive)
        {
            Destroy(GameObject.Find("Klon"));
            cloneAlive = false; 
        }
    }
	
	public float getDamage()
	{
		return damage;
	}

    public float getMaxHealth()
    {
        return maxHealth;
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

    public bool getCooldown2Active()
    {
        return cooldown2Active;
    }

    public void setCooldown2Active(bool value)
    {
        cooldown2Active = value;  
    }

    public float getCooldown2()
    {
        return cooldown2;
    }

    public void setTTL()
    {
        attl = ttl;
        cloneAlive = true;
    }

    //Returns difference between possible Stamina Damage and really done stamina dmg
    //eg. all damage can be absorbed into stamina = returns 0
    //10 Stamina left but 20  damage returns 10
    public float reduceStamina(float amount)
    {
        float potentialDamage = stamina - amount;
        stamina = Mathf.Clamp(stamina - amount, 0, maxStamina);
        if (potentialDamage < 0)
            return -1 * potentialDamage;
        else
            return 0;
    }
}
