using UnityEngine;
using System.Collections;

public class HealthEffectZone : MonoBehaviour {

	public float[] effectCooldown = {0.5f,0.5f};
	public float amount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (effectCooldown [0] < effectCooldown [1])
			effectCooldown [0] += Time.deltaTime;


	}
	void OnTriggerStay2D(Collider2D other)
	{
		if(effectCooldown[0] >= effectCooldown[1])
		{
			HealthSystem hs = (HealthSystem) other.gameObject.GetComponent(typeof(HealthSystem));
			if(hs != null)
			{
				if(amount >= 0)
					hs.raiseHealth(amount);
				else
					hs.lowerHealth(-amount);
				effectCooldown[0] = 0;
			}
		}
	}
}
