using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public enum DrawMode
    {
        Color,
        Noise
    }

    private bool m_AutoUpdate;
    private DrawMode m_DrawMode;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            DrawTerrainGeneration();
        }

        EditorGUI.BeginChangeCheck();

        m_AutoUpdate = EditorGUILayout.Toggle("Auto Update", m_AutoUpdate);
        m_DrawMode = (DrawMode)EditorGUILayout.EnumPopup("Draw Mode", m_DrawMode);

        // DrawDefaultInspector return true if something changed
        bool hasChanged = EditorGUI.EndChangeCheck();
        if (DrawDefaultInspector() || hasChanged)
        {
            if (m_AutoUpdate)
                DrawTerrainGeneration();
        }
    }

    private void DrawTerrainGeneration()
    {
        var generator = target as TerrainGenerator;
        var noiseMap = generator.GenerateNoiseMap();

        var display = FindObjectOfType<TerrainDisplay>();
        if (m_DrawMode == DrawMode.Noise)
        {
            display.DrawNoiseMap(noiseMap);
        }
        else if (m_DrawMode == DrawMode.Color)
        {
            display.DrawNoiseMapRegions(noiseMap, generator.regions);
        }
    }
}
