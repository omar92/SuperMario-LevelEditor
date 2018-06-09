using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public LevelMap level;
    public GameObject bg;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            level.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            level.Load();
            BuildMap();
        }
    }
    public void Start()
    {
        BuildMap();
    }
    Vector3 pos;
    private void BuildMap()
    {

        int width = (int)level.Size.x;
        int height = (int)level.Size.y;
        Debug.Log("BuildMap " + width + " " + height);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var item = level.GetElement(i, j);
                pos = new Vector3(i, j);
                if (item.DictionaryIndex > 0)
                    AddAsset(item, pos);
                //else
                //{
                //    var bgInst = GameObject.Instantiate<GameObject>(bg);
                //    pos.z += 1;
                //    bgInst.transform.position = pos;
                //    bgInst.name = pos.ToString();
                //    bgInst.transform.parent = this.transform;
                //}
            }
        }
    }

    private void AddAsset(MapElementStruct elementData, Vector3 position)
    {
        var assetData = level.assetsDictionary.MapAssets[elementData.DictionaryIndex - 1];
        var asset = GameObject.Instantiate<GameObject>(assetData.Prefab.gameObject);
        asset.transform.position = position;
        asset.name = position.ToString();
        asset.transform.parent = this.transform;
        asset.GetComponent<AMapElement>().Init(assetData.isStatic);
        asset.GetComponent<AMapElement>().SetData(elementData.Data);
    }



}
