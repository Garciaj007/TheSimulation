using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTimeOfDay : MonoBehaviour {

    /*Things to do:
    //Have a time method, or someway to attach the rotation of the sun move according to time
    //Have an animation curve to seamlessly transition from dawn/day/dusk/night
    //Allow it to be set manually, whether the speed of time, the transition, etc....
    */

    //Range for the rotational speed of the sun
    [Range(-5f, 5f)]
    public float sunRotationSpeed_x;
    //Range for +/- change
    [Range(0f, 0.001f)]
    public float change;
    //The procedural skybox material
    public Material skyBoxMaterial;
    //Visable Euler Angles
    public Vector3 rot;

    Light sunLight;
    float exposure, sunSize, atmosphereThickness, intensity;

    private void Start()
    {
        //Getting & Setting Components/Members
        sunLight = GetComponent<Light>();

        intensity = sunLight.intensity;
        exposure = skyBoxMaterial.GetFloat("_Exposure");
        sunSize = skyBoxMaterial.GetFloat("_SunSize");
        atmosphereThickness = skyBoxMaterial.GetFloat("_AtmosphereThickness");
    }
    
    private void Update() {
        //Used for refrence
        rot = transform.eulerAngles;
        
        //When the sun is entering the night cycle
        if (transform.eulerAngles.x > 90 || transform.eulerAngles.x < 10 )
        {
            //Night Cycle
            exposure -= change;
            intensity -= change;
            sunSize += change * 3;
            atmosphereThickness += change * 2;
        } else
        {
            //Day Cycle
            exposure += change;
            intensity += change;
            sunSize -= change * 3;
            atmosphereThickness -= change * 2;
        }


        //Limiting Values
        exposure = Limit(exposure);
        intensity = Limit(intensity);
        sunSize = Limit(sunSize, 0.15f, 0.4f);
        atmosphereThickness = Limit(atmosphereThickness, 0.5f, 1.5f);

        //Sets Suns intensity
        sunLight.intensity = intensity;
        //Sets Materials Values
        skyBoxMaterial.SetFloat("_Exposure", exposure);
        skyBoxMaterial.SetFloat("_SunSize", sunSize);
        skyBoxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
        //Rotates Sun
        transform.Rotate(sunRotationSpeed_x * Time.deltaTime, 0, 0);
    }

    //Limit between 0 to 1
    float Limit(float x)
    {
        if (x > 1)
            return 1;
        else if (x < 0)
            return 0;
        else
            return x;
    }

    //Limit between a min to a max
    float Limit(float x, float min, float max)
    {
        if (x > max)
            return max;
        else if (x < min)
            return min;
        else
            return x;
    }
}
