using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public bool autoUpdate;

    public int width;
    public int height;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public void DrawNoiseMap()
    {
        var noiseMap = NoiseMap.Generate(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);

        var display = FindObjectOfType<TerrainDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    // Call automatically when a script variables is changed in the inspector
    private void OnValidate()
    {
        if (width < 1)
            width = 1;

        if (height < 1)
            height = 1;

        if (lacunarity < 1)
            lacunarity = 1;

        if (octaves < 0)
            octaves = 0;
    }

}