using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {
    //Lebenspunkte, maximale Lebenspunkte, Rüstung und Schaden
    public float health, maxHealth, armor, damage;
    //Maximale Ausdauer, Ausdauer und regenerierte Ausdauer pro Sekunde
    public float maxStamina, stamina, staminaPerSecond;
    //Munition, Munitionskapazität und Reichweite
    //Range = 0 -> Nahkampf / Range > 0 -> Fernkampf
    public int ammo, ammoCap, range;
    //Klon aktiv?
    public bool cloneAlive = false;

    //Cooldown für Plasma-Schuss
    static float cooldown1 = 1.0f;
    //Läuft cooldown für Plasmaschuss?
    bool cooldown1Active = false;

    //Cooldown für Klonfähigkeit
    static float cooldown2 = 10.0f;
    //Time-to-live für Klon
    static float ttl = 5.0f;
    //Aktuelle Time-To-Live
    float attl = ttl;
    //Läuft cooldown für Klon?
    bool cooldown2Active = false;

    MeleeSystem meleeSys;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        stamina = maxStamina = 100f;
        
        meleeSys = (MeleeSystem)GetComponent(typeof(MeleeSystem));
	}
	
	// Update is called once per frame
	void Update () {

        /*Fülle Ausdauert über Zeit wieder auf solange maximalStamina nicht erreicht ist und die
        Spielfigur sich nicht bewegt*/
        if (staminaPerSecond > 0.0f && stamina < maxStamina && !meleeSys.animationRunning)
        {
            //Stelle sicher, dass Stamina nicht kleiner als 0 oder größer als maximalStamina gesetzt wird
            stamina = Mathf.Clamp(stamina+staminaPerSecond * Time.deltaTime,0,maxStamina);
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
