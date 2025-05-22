using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


/// <summary>
///     This script makes the MapLayer class a scriptable object
///     To create a new MapLayer, right click in the project view and select Map Layer Settings
///     You can then use this script with the LevelGenerator to generate your level
public enum Algorithm
{
    Perlin,
    PerlinSmoothed,
    PerlinCave,
    RandomWalkTop,
    RandomWalkTopSmoothed,
    RandomWalkCave,
    RandomWalkCaveCustom,
    CellularAutomataVonNeuman,
    CellularAutomataMoore,
    DirectionalTunnel
}

[Serializable]
[CreateAssetMenu(fileName = "NewMapSettings", menuName = "Map Settings", order = 0)]
public class MapSettings : ScriptableObject
{
    public Algorithm algorithm;
    public bool randomSeed;
    public float seed;
    public int fillAmount;
    public int smoothAmount;
    public int clearAmount;
    public int interval;
    public int minPathWidth, maxPathWidth, maxPathChange, roughness, windyness;
    public bool edgesAreWalls;
    public float modifier;
}

//Custom UI for our class
[CustomEditor(typeof(MapSettings))]
public class MapSettings_Editor : Editor
{
    // private ReorderableList tileList;
    private MapSettings mapLayer;
    // bool populateTileList = false;

    // private void InitTileList()
    // {
    //     populateTileList = mapLayer.tiles?.Count > 0;
    //     if (!populateTileList) return;
    //     tileList = new ReorderableList(serializedObject,
    //         serializedObject.FindProperty("tiles"),
    //         true, true, true, true);
    //     tileList.drawElementCallback = 
    //         (Rect rect, int index, bool isActive, bool isFocused) =>
    //         {
    //             var element = tileList.serializedProperty.GetArrayElementAtIndex(index);
    //             rect.y += 2;
    //             EditorGUI.PropertyField(
    //                 new Rect(rect.x, rect.y, Screen.width*.8f, EditorGUIUtility.singleLineHeight),
    //                 element, GUIContent.none);
    //         };
    //
    //     tileList.onAddCallback = (ReorderableList l) =>
    //     {
    //         var index = l.serializedProperty.arraySize;
    //         l.serializedProperty.arraySize++;
    //         l.index = index;
    //         var element = l.serializedProperty.GetArrayElementAtIndex(index);
    //     };
    // }
    //
    private void OnEnable()
    {
        mapLayer = (MapSettings)target;
        //this.InitTileList();
    }

    public override void OnInspectorGUI()
    {
        mapLayer = (MapSettings)target;
        GUI.changed = false;
        EditorGUILayout.LabelField(mapLayer.name, EditorStyles.boldLabel);
        //tileList = new ReorderableList(mapLayer.tiles ,typeof(TileBase));

        // serializedObject.Update();
        // tileList.DoLayoutList();
        // serializedObject.ApplyModifiedProperties();

        //tiles  = EditorGUILayout. ("Interval Of Points", mapLayer.interval, 1, 10);
        EditorGUI.BeginChangeCheck();
        if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
        //Run Operations to update values in script and associated elements
        //Display List
        // if (populateTileList)
        // {
        //     tileList.DoLayoutList();
        // }
        mapLayer.algorithm =
            (Algorithm)EditorGUILayout.EnumPopup(
                new GUIContent("Generation Method", "The generation method we want to use to generate the map"),
                mapLayer.algorithm);
        mapLayer.randomSeed = EditorGUILayout.Toggle("Random Seed", mapLayer.randomSeed);

        //Only appear if we have the random seed set to false
        if (!mapLayer.randomSeed) mapLayer.seed = EditorGUILayout.FloatField("Seed", mapLayer.seed);

        //Shows different options depending on what algorithm is selected
        switch (mapLayer.algorithm)
        {
            case Algorithm.Perlin:
                //No additional Variables
                break;
            case Algorithm.PerlinSmoothed:
                mapLayer.interval = EditorGUILayout.IntSlider("Interval Of Points", mapLayer.interval, 1, 10);
                break;
            case Algorithm.PerlinCave:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.modifier = EditorGUILayout.Slider("Modifier", mapLayer.modifier, 0.0001f, 1.0f);
                break;
            case Algorithm.RandomWalkTop:
                //No additional Variables
                break;
            case Algorithm.RandomWalkTopSmoothed:
                mapLayer.interval = EditorGUILayout.IntSlider("Minimum Section Length", mapLayer.interval, 1, 10);
                break;
            case Algorithm.RandomWalkCave:
                mapLayer.clearAmount = EditorGUILayout.IntSlider("Amount To Clear", mapLayer.clearAmount, 0, 100);
                break;
            case Algorithm.RandomWalkCaveCustom:
                mapLayer.clearAmount = EditorGUILayout.IntSlider("Amount To Clear", mapLayer.clearAmount, 0, 100);
                break;
            case Algorithm.CellularAutomataVonNeuman:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.fillAmount = EditorGUILayout.IntSlider("Fill Percentage", mapLayer.fillAmount, 0, 100);
                mapLayer.smoothAmount = EditorGUILayout.IntSlider("Smooth Amount", mapLayer.smoothAmount, 0, 10);
                break;
            case Algorithm.CellularAutomataMoore:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.fillAmount = EditorGUILayout.IntSlider("Fill Percentage", mapLayer.fillAmount, 0, 100);
                mapLayer.smoothAmount = EditorGUILayout.IntSlider("Smooth Amount", mapLayer.smoothAmount, 0, 10);
                break;
            case Algorithm.DirectionalTunnel:
                mapLayer.minPathWidth = EditorGUILayout.IntField("Minimum Path Width", mapLayer.minPathWidth);
                mapLayer.maxPathWidth = EditorGUILayout.IntField("Maximum Path Width", mapLayer.maxPathWidth);
                mapLayer.maxPathChange = EditorGUILayout.IntField("Maximum Path Change", mapLayer.maxPathChange);
                mapLayer.windyness = EditorGUILayout.IntSlider(
                    new GUIContent("Windyness",
                        "This is checked against a random number to determine if we can change the paths current x position"),
                    mapLayer.windyness, 0, 100);
                mapLayer.roughness = EditorGUILayout.IntSlider(
                    new GUIContent("Roughness",
                        "This is checked against a random number to determine if we can change the width of the tunnel"),
                    mapLayer.roughness, 0, 100);
                break;
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        AssetDatabase.SaveAssets();

        if (GUI.changed)
            EditorUtility.SetDirty(mapLayer);
    }
}
#endif