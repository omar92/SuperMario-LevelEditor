using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "Map", menuName = "GameLevel/Map", order = 1)]
public class LevelMap : ScriptableObject
{
    private MapElementStruct[,] map;
    public MapAssetsDictionary assetsDictionary;
    private Vector2 size;
    public bool initialised = false;

    [TextArea()]
    public string data;
    public bool Initialised
    {
        get
        {
            return initialised;
        }
    }

    public Vector2 Size
    {
        get
        {
            return size;
        }
    }



    //public MapElementStruct[,] Map
    //{
    //    get
    //    {
    //        if (map == null)
    //        {
    //            if (!initialised)
    //            {
    //                InitMap((int)size.x, (int)size.y);
    //            }
    //            else
    //            {
    //                Load();
    //            }
    //        }
    //        return map;
    //    }
    //}

    public void InitMap(Vector2 size)
    {
        Debug.Log("init");
        this.size = size;
        map = new MapElementStruct[(int)size.x, (int)size.y];
        data = "";
        data += (size.x) + "\n";
        data += (size.y) + "\n";

        initialised = true;
        //  Save();
    }
    public void InitMap()
    {
        InitMap(size);
    }

    internal MapElementStruct GetElement(Vector3 position)
    {
        return GetElement((int)position.x, (int)position.y);
    }
    internal MapElementStruct GetElement(int x, int y)
    {
        if (map == null)
        {
            if (!initialised)
            {
                InitMap();
            }
            else
            {
                Load();
            }
        }
        return map[x, y];
    }


    public bool CheckIfBuried(Vector3 position)
    {
     //   Debug.Log("__________________________________-");
      //  Debug.Log("position " + position);
        Vector3 checkPos;
        int checkIndex = 0;
        //check up 
        checkPos = (position + Vector3.up);
      //  Debug.Log("up " + checkPos);
        if (checkPos.y < size.y)
        {
        //    Debug.Log("up ID " + GetElement(checkPos).DictionaryIndex);
            checkIndex = GetElement(checkPos).DictionaryIndex;
            if (checkIndex  == 0 || !assetsDictionary.MapAssets[checkIndex-1].isStatic) return false;
        }

        //check downs 
        checkPos = (position + Vector3.down);
      //  Debug.Log("down " + checkPos);
        if (checkPos.y >= 0)
        {
        //    Debug.Log("down ID " + GetElement(checkPos).DictionaryIndex);
            checkIndex = GetElement(checkPos).DictionaryIndex;
            if (checkIndex == 0 || !assetsDictionary.MapAssets[checkIndex - 1].isStatic) return false;
        }



        //check right 
        checkPos = (position + Vector3.right);
      //  Debug.Log("right " + checkPos);
        if (checkPos.x < size.x)
        {
         ///   Debug.Log("right ID " + GetElement(checkPos).DictionaryIndex);
            checkIndex = GetElement(checkPos).DictionaryIndex;
            if (checkIndex == 0 || !assetsDictionary.MapAssets[checkIndex - 1].isStatic) return false;
        }



        //check left  
        checkPos = (position + Vector3.left);
       // Debug.Log("left " + checkPos);
        if (checkPos.x >= 0)
        {
        //    Debug.Log("left ID " + GetElement(checkPos).DictionaryIndex);
            checkIndex = GetElement(checkPos).DictionaryIndex;
            if (checkIndex == 0 || !assetsDictionary.MapAssets[checkIndex - 1].isStatic) return false;
        }

       // Debug.Log("__________________________________-");
        return true;
    }

    internal void UpdateElement(Vector3 position, int index, string data)
    {
        Debug.Log("update p:" + position + " data:" + data);
        if (map == null)
        {
            if (!initialised)
            {
                InitMap();
            }
            else
            {
                Load();
            }
        }

        map[(int)position.x, (int)position.y] = new MapElementStruct
        {
            DictionaryIndex = index,
            Data = data
        };
    }

    internal void RemoveElement(Vector3 position)
    {
        UpdateElement(position, 0, "");
    }

    public void Save()
    {
        data = "";
        int width = (int)size.x;
        int height = (int)size.y;
        data += width + "\n";
        data += height + "\n";

        var elementData = "";
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y].DictionaryIndex > 0)
                {
                    elementData = "";
                    elementData += x + ",";
                    elementData += y + ",";
                    elementData += map[x, y].DictionaryIndex + ",";
                    elementData += map[x, y].Data;

                    data += elementData + "\n";
                }
            }
        }
        Debug.Log("save  Level" + name);
        Debug.Log(data);
        Save(data);
    }
    public void Save(Transform MapRoot)
    {
        data = "";
        int width = map.GetUpperBound(0);
        int height = map.GetUpperBound(1);
        data += width + "\n";
        data += height + "\n";

        var elementData = "";
        AMapElement element = null;
        for (int i = 0; i <= MapRoot.childCount; i++)
        {
            elementData = "";
            {
                element = MapRoot.GetChild(i).GetComponent<AMapElement>();
                elementData += (int)element.transform.localPosition.x + ",";
                elementData += (int)element.transform.localPosition.y + ",";
                elementData += map[(int)element.transform.localPosition.x, (int)element.transform.localPosition.x].DictionaryIndex + ",";
                elementData += map[(int)element.transform.localPosition.x, (int)element.transform.localPosition.x].Data;
            }
            data += elementData + "\n";
        }
        Debug.Log("save  Level" + name);
        Debug.Log(data);
        Save(data);
    }
    public void Save(string data)
    {
        PlayerPrefs.SetString("Level" + name, data);
    }

    public void Load()
    {
        Debug.Log("load  Level" + name);
        data = PlayerPrefs.GetString("Level" + name);
        Debug.Log(data);

        var dataArray = data.Split('\n');
        int width = int.Parse(dataArray[0]);
        int height = int.Parse(dataArray[1]);
        size = new Vector2(width, height);
        map = new MapElementStruct[width, height];

        string[] innerDataArray;
        int x;
        int y;

        for (int i = 2; i < dataArray.Length - 1; i++)
        {
            //  Debug.Log(dataArray[i]);
            innerDataArray = dataArray[i].Split(',');

            x = int.Parse(innerDataArray[0]);
            y = int.Parse(innerDataArray[1]);
            map[x, y] = new MapElementStruct
            {
                DictionaryIndex = int.Parse(innerDataArray[2]),
                Data = innerDataArray[3]
            };
        }
    }
}

public struct MapElementStruct
{
    public int DictionaryIndex;
    public string Data;
}