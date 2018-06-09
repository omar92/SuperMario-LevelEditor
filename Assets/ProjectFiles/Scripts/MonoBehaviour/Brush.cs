using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{

    public LevelMap level;
    public TransformVariable MapParent;
    public FloatVariable selectedElementIndex;
    public TransformVariable selectedTransform;
    // Use this for initialization
    void Awake()
    {
        level.Load();
    }

    // Update is called once per frame
    void Update()
    {
        int width = (int)level.Size.x;
        int height = (int)level.Size.y;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x - 1 >= 0)
                transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x + 1 < width)
                transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.y + 1 < height)
                transform.position += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.y - 1 >= 0)
                transform.position += Vector3.down;
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            MapElementStruct cellVal = level.GetElement(transform.position);

            if (cellVal.DictionaryIndex > 0)
            {
                RemoveAsset(transform.position);
            }
            AddAsset((int)selectedElementIndex.value, selectedTransform.value.GetComponent<AMapElement>().GetData(), transform.position);
            level.UpdateElement(transform.position, ((int)selectedElementIndex.value + 1), selectedTransform.value.GetComponent<AMapElement>().GetData());

        
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            var cellVal = level.GetElement(transform.position);
            if (cellVal.DictionaryIndex > 0)
            {
                RemoveAsset(transform.position);
            }
            level.RemoveElement(transform.position);

        }
        //for (int i = 0; i <= level.assetsDictionary.MapAssets.Count; i++)
        //{
        //    if (Input.GetKeyDown(i + ""))
        //    {

        //    }
        //}
    }

    private void AddAsset(int assetIndex, string data, Vector3 position)
    {
        var assetData = level.assetsDictionary.MapAssets[assetIndex];
        var asset = GameObject.Instantiate<GameObject>(assetData.Prefab.gameObject); 
        asset.transform.position = position;
        asset.name = position.ToString();
        asset.transform.parent = MapParent.value;
        asset.GetComponent<AMapElement>().Init(assetData.isStatic);
        asset.GetComponent<AMapElement>().SetData(data);
    }

    private void RemoveAsset(Vector3 position)
    {
        var asset = GameObject.Find(position.ToString());
        Destroy(asset);
    }



    //private void OnDestroy()
    //{
    // //   level.Save();
    //}
}
