using UnityEngine;
using System.Collections;

public class NavigationSystem : MonoBehaviour {

	BoxCollider2D playercoll;
	BoxCollider2D telecoll;
	//Setup a default blank texture for fading if none is supplied
	Material fadeMaterial;

	// Use this for initialization
	void Start () {
		playercoll = (BoxCollider2D)GameObject.FindGameObjectWithTag ("Player").GetComponent (typeof(BoxCollider2D));
		telecoll = (BoxCollider2D)this.gameObject.GetComponent (typeof(BoxCollider2D));
		//Setup a default blank texture for fading if none is supplied
		fadeMaterial = new Material("Shader \"Plane/No zTest\" {" +
		                            "SubShader { Pass { " +
		                            " Blend SrcAlpha OneMinusSrcAlpha " +
		                            " ZWrite Off Cull Off Fog { Mode Off } " +
		                            " BindChannels {" +
		                            " Bind \"color\", color }" +
		                            "} } }");
		StartCoroutine (FadeOut ());

	}
	

	
	// Update is called once per frame
	void Update () {
		if (playercoll.IsTouching (telecoll) && Input.GetKey ("t")) {
			StartCoroutine(FadeIn());
		}

	}
	private IEnumerator FadeIn()
	{
		float t = 0.0f;
		while (t < 1.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(
				t + Time.deltaTime / 1);
			DrawQuad(
				fadeMaterial,
				Color.black,
				t);
		}
		Application.LoadLevel (this.gameObject.name);
	}

	public IEnumerator FadeOut()
	{
		float t = 1.0f;
			while (t > 0.0f) {
				yield return new WaitForEndOfFrame ();
				t = Mathf.Clamp01 (t - Time.deltaTime / 1);
				DrawQuad (
					fadeMaterial,
					Color.black,
					t);
			}
	}


	
	public static void DrawQuad(
		Material aMaterial,
		Color aColor,
		float aAlpha)
	{
		aColor.a = aAlpha;
		aMaterial.SetPass(0);
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(aColor);
		GL.Vertex3(0, 0, -1);
		GL.Vertex3(0, 1, -1);
		GL.Vertex3(1, 1, -1);
		GL.Vertex3(1, 0, -1);
		GL.End();
		GL.PopMatrix();
	}
}
