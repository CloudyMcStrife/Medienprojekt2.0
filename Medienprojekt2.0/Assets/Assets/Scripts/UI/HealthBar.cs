using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	Vector2 position = new Vector2(20,20);
	Vector2 size = new Vector2(200, 20);


	float barFill = 0.8f;
	string barText = "health";

	public GUIStyle progressEmpty;
	public GUIStyle progressFull;
	// Use this for initialization
	void OnGUI () {
		GUI.BeginGroup (new Rect (position.x, position.y, size.x, size.y));

		GUI.Box (new Rect (0, 0, size.x, size.y), barText, progressEmpty);
		GUI.Box (new Rect (0, 0, size.x * barFill, size.y), barText, progressFull);

		GUI.EndGroup ();
	
	}
	
	// Update is called once per frame
	void Update () {
		AttributeComponent ac = this.GetComponent<AttributeComponent> ();

		float health = ac.getHealth ();
		barFill =  health + 8 / 10;
	}
}
