using UnityEngine;
using System.Collections;

public class ThunderComponent : MonoBehaviour {

	// Use this for initialization

    public float offDurationMin;
    public float offDurationMax;

    public float onDurationMin;
    public float onDurationMax;

    Light l;

    void Start()
    {
        l = GetComponent<Light>();
        StartCoroutine(DoOnOff());
    }

    IEnumerator DoOnOff()
    {
        while (true)
        {
            l.enabled = false;
            yield return new WaitForSeconds(Random.Range(offDurationMin, offDurationMax));
            l.enabled = true;
            yield return new WaitForSeconds(Random.Range(onDurationMin, onDurationMax));
        }
    }

}
