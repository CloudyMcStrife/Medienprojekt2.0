using UnityEngine;
using System.Collections;

//Beschreibt alle Aktionen die Gegner ausführen können

public class EnemyMovement : MonoBehaviour {

	Rigidbody2D rigplayer;
	Rigidbody2D rigenemy;
	public float noticeDistance;
	public float minimumDistancex;
	public float minimumDistancey;
	public bool inAttackRangex;
	public bool inAttackRangey;
	public bool inNoticeRadius;
	float distancex;
	float distancey;
	float speed = 1.0f;
	public float[] attackCooldown = {1.0f,1.0f};
	Projectile currentProjectile;
	ProjectilePoolingSystem PPS;
	// Use this for initialization
	void Awake () {
		rigplayer  = (Rigidbody2D) GameObject.FindWithTag("Player").GetComponent(typeof(Rigidbody2D));
		rigenemy  = (Rigidbody2D) GetComponent(typeof(Rigidbody2D));
		PPS = (ProjectilePoolingSystem)GetComponent (typeof(ProjectilePoolingSystem));
	}
	
	// Update is called once per frame
	void Update () {
		if (attackCooldown [0] < attackCooldown [1]) {
			attackCooldown [0] += Time.deltaTime;
		}
		distancex = rigplayer.position.x - rigenemy.position.x;
		distancey = rigplayer.position.y - rigenemy.position.y;
		//prüft ob EnemyEntity links oder rechts in minimumDistance (=Angriffsreichweite) ist;
		inAttackRangex = (distancex <= minimumDistancex && distancex > 0) || (distancex >= -minimumDistancex && distancex < 0);
		inAttackRangey = (distancey <= minimumDistancey && distancey > 0) || (distancey >= -minimumDistancey && distancey < 0);

		inNoticeRadius = (distancex <= noticeDistance && distancex > 0) || (distancex >= -noticeDistance && distancex < 0);
		//Debug.Log (inNoticeRadius);

		if (inNoticeRadius && !inAttackRangex) {
			if (distancex > 0)
				rigenemy.velocity = new Vector2 (speed, 0);
			if (distancex < 0)
				rigenemy.velocity = new Vector2 (-speed, 0);
		} else
			rigenemy.velocity = new Vector2 (0, rigenemy.velocity.y);

		if (inAttackRangex && inAttackRangey) {
			if(attackCooldown[0] >= attackCooldown[1])
			{
				//Setzt das Script auf das Projektil das als nächstes Geschossen werden soll(Oberstes des PPS)
				GameObject projectile = PPS.getProjectile();
				if(projectile != null)
				{
					currentProjectile = (Projectile) projectile.GetComponent(typeof(Projectile));
					attackCooldown[0] = 0;
					currentProjectile.shoot(rigenemy.gameObject,3.0f);
				}
			}
		}
	}

	public bool isFacingRight()
	{
		return distancex > 0;
	}
}
