using UnityEngine;
using System.Collections;

public class RangedSystem : MonoBehaviour {
    Projectile currentProjectile;
    ProjectilePoolingSystem PPS;
    AttributeComponent attributes;
    Animator anim;
    CharacterMovement movement;
    public GameObject shotSound;
    public GameObject plasmaSound;
    public float shootingRange;

    public float[] rangeAttackCooldown = { 1.0f, 1.0f };
    bool shotAnimationReady;
    
	// Use this for initialization
	void Start () {
        PPS = (ProjectilePoolingSystem)GetComponent(typeof(ProjectilePoolingSystem));
        attributes = (AttributeComponent)GetComponent(typeof(AttributeComponent));
        movement = (CharacterMovement)GetComponent(typeof(CharacterMovement));
        anim = (Animator)GetComponent(typeof(Animator));
        shotAnimationReady = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (rangeAttackCooldown[0] < rangeAttackCooldown[1])
            rangeAttackCooldown[0] += Time.deltaTime;
	}

    public IEnumerator shoot(bool is_normal_shot)
    {
		if (is_normal_shot || (!is_normal_shot && attributes.getAmmo() > 0))
		{
	        if (rangeAttackCooldown[0] >= rangeAttackCooldown[1])
	        {
				rangeAttackCooldown[0] = 0;
				if (anim != null)
	                anim.SetTrigger("Shot");
	            while (!shotAnimationReady)
	            {
	                yield return null;
	            }
	            GameObject proj = PPS.getProjectile();
	            shotAnimationReady = false;
	            if (proj != null)
	            {
	                currentProjectile = proj.GetComponent<Projectile>();
                    if (is_normal_shot)
                        Instantiate(shotSound);
                    else
                        Instantiate(plasmaSound);
                    if (is_normal_shot)
	                {
	                    currentProjectile.set_shooting_type(Projectile.Shooting_Type.NORMAL);
	                    currentProjectile.shoot(shootingRange, movement.facingRight);
	                }
	                else if (!is_normal_shot && attributes.getAmmo() > 0)
	                {
	                    currentProjectile.set_shooting_type(Projectile.Shooting_Type.SPECIAL);
	                    attributes.decrementAmmo();
	                    currentProjectile.shoot(shootingRange, movement.facingRight);

	                }
	                
	            }
			}
        }
    }

    //Methode auf die der Keyframe zugreift, um den Schuss zu ermöglichen
    public void setShotAnimationReady()
    {
        shotAnimationReady = true;
    }

    //Methode für Keyframe, die angibt, wann ein neuer Schuss gequeued werden kann
    public void nextShotReady()
    {
        anim.SetBool("AttackInProgress", false);
    }


    //Waffe wechseln
    public void switchWeapon()
    {
        AmmoIconHandler iconHandler = GameObject.Find("HUD").GetComponent<AmmoIconHandler>();
        if(iconHandler!=null)
            iconHandler.changeAmmo();

    }
}
