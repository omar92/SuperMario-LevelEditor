using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDictionary", menuName = "GameLevel/MapDictionary", order = 1)]
public class MapAssetsDictionary : ScriptableObject
{
    public List<MapAsset> MapAssets = new List<MapAsset>();

    public List<MapAsset> MapAssets1 { get { if (MapAssets == null) { MapAssets = new List<MapAsset>(); } return MapAssets; } set { MapAssets = value; } }
}
[System.Serializable]
public struct MapAsset
{
    public string name;
    //public int key;
    public AMapElement Prefab;
    public bool isStatic;
}