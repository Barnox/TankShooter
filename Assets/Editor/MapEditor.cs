using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

public class MapEditor : OdinEditorWindow
{


    [MenuItem("MapTools/MapEditor")]
    private static void OpenWindow()
    {
        GetWindow<MapEditor>().Show();
    }

     [TableMatrix(HorizontalTitle = "X axis", VerticalTitle = "Y axis")]
     public TerrainTable[,] LabledMatrix = new TerrainTable[6, 6];

        /*
    [TableList(ShowIndexLabels = true)]
    public List<TerrainTable> TableListWithIndexLabels = new List<TerrainTable>()
{
    new TerrainTable(),
    new TerrainTable(),
};
    */

    public class TerrainTable
    {
        [TableColumnWidth(60, Resizable = true)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]


        [VerticalGroup("Terrain Tile")]
        [AssetSelector(Paths = "Assets/Tanks/Tank Parts")]
        public ScriptableObject ScriptableObjectsFromFolder;

        [VerticalGroup("Terrain Tile")]
        public int cellRotation = 0;

        [VerticalGroup("Terrain Tile")]
        public bool cellWalkable = false;

    }


}
