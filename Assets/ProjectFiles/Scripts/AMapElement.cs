using System;
using UnityEngine;
public abstract class AMapElement : MonoBehaviour
{
    Collider col;
    //  Renderer rend;
    public BoolVariable isEditMode;
    public LevelMap map;
    bool colEnabled = false;
    public void Init(bool isStatic)
    {
        col = GetComponent<Collider>();
        if (isStatic && col)
        {
            if (isEditMode.value)
            {
                colEnabled = false;
            }
            else
            {
                colEnabled = !map.CheckIfBuried(transform.position);
            }
            col.enabled = colEnabled;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = colEnabled ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }


    public abstract void SetData(string data);
    public abstract string GetData();

    // public abstract int GetDictionaryIndex();
    // public abstract int SetDictionaryIndex(int index);

}