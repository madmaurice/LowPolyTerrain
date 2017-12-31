using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoiseMap
{
    private float[,] data;

    public int width;
    public int height;
    public int seed;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public float scale;
    public Vector2 offset;

    public float this[int x, int y]
    {
        get { return data[x, y]; }
        private set { data[x, y] = value; }
    }

    public static NoiseMap Generate(int width, int height, int seed, float scale, 
        int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        var noiseMap = new NoiseMap() { width = width, height = height, seed = seed, scale = scale,
            octaves = octaves, persistance = persistance, lacunarity = lacunarity, offset = offset };
        noiseMap.data = new float[width, height];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
    
        /*
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;
        */

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < noiseMap.height; y++)
        {
            for (int x = 0; x < noiseMap.width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    var ocatveOffset = octaveOffsets[i];
                    float sampleX = x / scale * frequency + ocatveOffset.x;
                    float sampleY = y / scale * frequency + ocatveOffset.y;

                    // *2 and -1 to get a range of [-1, 1]
                    // This gives a better noise across octaves since noiseHeight can be subtracted < 0
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalize noiseMap [0, 1]
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}