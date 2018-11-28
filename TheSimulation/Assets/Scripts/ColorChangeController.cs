using UnityEngine;

public class ColorChangeController : MonoBehaviour
{
    public enum ColorDestination { light, material, particle };
    public ColorDestination destination;
    [Range(0.0f, 0.2f)]
    public float speed;
    [Range(0.0f, 1.0f)]
    public float initColor = 0f;

    [Header("Material Only")]
    [Range(0.0f, 4.0f)]
    public float brightnessFactor;
    
    private ParticleSystem.MainModule p_main;
    private Color color;
    private float hue, sat, val;

    private void Start()
    {
        p_main = GetComponent<ParticleSystem>().main;

        if (initColor != 0)
            color = Color.HSVToRGB(initColor, 1, 1);
        else
            color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);

        Color.RGBToHSV(color, out hue, out sat, out val);
    }

    private void Update()
    {
        if (destination == ColorDestination.light)
        {
            GetComponent<Light>().color = color;
        }

        if (destination == ColorDestination.material)
        {
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color * brightnessFactor);
        }

        if (destination == ColorDestination.particle)
        {
            p_main.startColor = color;
        }
    }

    private void LateUpdate()
    {
        hue += speed * Time.deltaTime;

        if (hue > 1)
            hue = 0;

        color = Color.HSVToRGB(hue, 1, 1);
    }
}