using UnityEngine;

public class ManaField : MonoBehaviour {

    public Rigidbody manaTransform;
    public Vector3 offset;
    public Vector3 dimensions;
    public float scale;
    [Range(-4f, 4f)]
    public float contrast;
    [Range(-2f, 2f)]
    public float level;
    [Header("Debugging")]
    public bool display;
    public Texture2D[] textures;

    public static ManaField Instance { get; private set; }

    // Use this for initialization
    void Start () {
        Instance = this;
        textures = new Texture2D[(int)dimensions.z];
        manaTransform.transform.position = offset;
	}
	
	// Update is called once per frame
	void Update () {
        manaTransform.AddForce(Random.onUnitSphere);
        offset = manaTransform.transform.position;

        if (display)
        {
            for(int i = 0; i < dimensions.z; i++)
            {
                textures[i] = GenerateTexture(i);
            }
        }
	}

    public float GetManaSample(Vector3 position)
    {
        Vector3 totalPosition = position + offset;
        Vector3 scaled = totalPosition * scale;
        return Perlin3D(scaled.x, scaled.y, scaled.x) * contrast + level;
    }

    private Texture2D GenerateTexture(int z)
    {
        Texture2D texture = new Texture2D((int)dimensions.x, (int)dimensions.y);

        for(int x  = 0; x < dimensions.x; x++)
        {
            for(int y = 0; y < dimensions.y; y++)
            {
                texture.SetPixel(x, y, CalculateColor(x, y, z));
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y, int z)
    {
        float xCoord = x / dimensions.x;
        float yCoord = y / dimensions.y;
        float zCoord = z / dimensions.z;

        float sample = GetManaSample(new Vector3(xCoord, yCoord, zCoord));
        return new Color(sample, sample, sample);
    }

    private float Perlin3D(float x, float y, float z)
    {
        float AB = Mathf.PerlinNoise(x, y);
        float BC = Mathf.PerlinNoise(y, z);
        float AC = Mathf.PerlinNoise(x, z);

        float BA = Mathf.PerlinNoise(y, x);
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, x);

        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f;
    }
}
