using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var generator = target as TerrainGenerator;

        // DrawDefaultInspector return true if something changed
        if (DrawDefaultInspector())
        {
            if (generator.autoUpdate)
                generator.DrawNoiseMap();
        }

        if (GUILayout.Button("Generate"))
        {
            generator.DrawNoiseMap();
        }
    }
}
