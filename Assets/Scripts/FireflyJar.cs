using UnityEngine;
using System.Collections;

public class FireflyJar : MonoBehaviour {

    public int fly_count;
    public int max_fly_count;
    public float intensity_increase = 0.5f;

    public DimLights dl;

    public void addFly()
    {
        Light lt = GetComponent<Light>();

        if(fly_count < max_fly_count)
        {
            lt.intensity += intensity_increase;
            fly_count++;
        }

        dl.dim();
    }
}
