using UnityEngine;
using System.Collections;

public class DimLights : MonoBehaviour {

    private float t;
    private float fadeStart, fadeEnd;
    private Light l;
    private bool done = true;

    public float fadeTime;
    public float step;

    public void Start () 
    {
        l = GetComponent<Light>();
    }


    public void dim() 
    {
        t = Time.deltaTime;
        fadeStart = l.intensity;
        fadeEnd = fadeStart - step;
        done = false;
    }

    private void Update() 
    {
        if (!done)
        {
            t += Time.deltaTime;


            l.intensity = fadeStart - (t / fadeTime) * step;
            if (t>fadeTime)
                done = true;
            
        }

    }

}
