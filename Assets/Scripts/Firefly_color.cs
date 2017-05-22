using UnityEngine;
using System.Collections;

public class Firefly_color : MonoBehaviour {
    private int current_color;
    private bool color_direction_up;
    public float intensity_max;
    public int intensity_interval;

    // Use this for initialization
    void Start () {
        current_color = Random.Range(0, 51)*5; // get a random between 0-255, with common denominator of 5
    }
	
	// Update is called once per frame
	void Update () {
        if (color_direction_up )
        {
            if (current_color < 255)
                current_color += intensity_interval;
            else
            {
                current_color = 255;
                color_direction_up = !color_direction_up;
            }
        }   
        else
        {
            if (current_color > 0)
                current_color -= intensity_interval;
            else
                color_direction_up = !color_direction_up;
        }

        GetComponent<Light>().intensity = (float)current_color / 255 * intensity_max;
        GetComponent<SpriteRenderer>().color = new Color32((byte)current_color, (byte)current_color, (byte)current_color, 255);
        GetComponent<TrailRenderer>().time = (float)current_color / 255;
    }
}
