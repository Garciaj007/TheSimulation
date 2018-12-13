using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    //IGNORE

    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public int iterations = 5;
    public float scale = 1;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()
    {
        Terrain t = GetComponent<Terrain>();
        t.terrainData = GenerateTerrain(t.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float avg = 0;

        for(int i = 1; i < iterations; i++)
        {
            float xCoord = (float)x / width * Mathf.Pow(scale, i);
            float yCoord = (float)y / height * Mathf.Pow(scale, i);

            avg += Mathf.PerlinNoise(xCoord, yCoord);
        }

        avg /= iterations;

        return avg;
    }
}
