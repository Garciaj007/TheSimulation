using UnityEngine;
using UnityEngine.UI;

public class ColorChangeController : MonoBehaviour
{
    public enum ColorDestination { Light, Sprite, Material, Particle };
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
    private float hue;

    private void Start()
    {
        float sat, val;

        p_main = GetComponent<ParticleSystem>().main;

        if (initColor != 0)
            color = Color.HSVToRGB(initColor, 1, 1);
        else
            color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);

        Color.RGBToHSV(color, out hue, out sat, out val);
    }

    private void Update()
    {
        if (destination == ColorDestination.Light)
            GetComponent<Light>().color = color;

        if (destination == ColorDestination.Sprite)
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, GetComponent<Image>().color.a);

        if (destination == ColorDestination.Material)
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color * brightnessFactor);

        if (destination == ColorDestination.Particle)
            p_main.startColor = color;
    }

    private void LateUpdate()
    {
        hue += speed * Time.deltaTime;

        if (hue > 1)
            hue = 0;

        color = Color.HSVToRGB(hue, 1, 1);
    }
}