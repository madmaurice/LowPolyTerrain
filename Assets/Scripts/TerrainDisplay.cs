using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void DrawNoiseMap(NoiseMap noiseMap)
    {
        int width = noiseMap.width;
        int height = noiseMap.height;

        Texture2D texture = new Texture2D(width, height);
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();

        // sharedMaterial allow to not enter game mode (textureRender.material only instantiated at runtime)
        textureRender.sharedMaterial.mainTexture = texture;

        // Set size of the plane to match
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}
