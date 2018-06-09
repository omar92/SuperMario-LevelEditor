using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    public LevelMap level;
    public FloatVariable selectedIndex;
    public TransformVariable selectedTransform;
    public Transform SelectedArea;
   // Transform selected;

    //int selectedIndex = 0;
    // Use this for initialization
    void Start()
    {
        selectedIndex.value = 0;
        SelectElementAt((int)selectedIndex.value);
    }

    private void SelectElementAt(int index)
    {
        if (selectedTransform.value != null)
        {
            Destroy(selectedTransform.value.gameObject);
        }
        selectedTransform.value = Instantiate<Transform>(level.assetsDictionary.MapAssets[index].Prefab.transform);
      //  selectedTransform.value = selectedTransform.value;
        if (selectedTransform.value)
        {
            selectedTransform.value.parent = SelectedArea;
            selectedTransform.value.localPosition = Vector3.zero;
            var selectable = selectedTransform.value.GetComponent<ISelectable>();
            if (selectable != null)
            {
                selectable.OnUI(true);
            }
        }
       // print("PreviewLayer " + SelectedArea.gameObject.layer);
       // selected.gameObject.layer = SelectedArea.gameObject.layer;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedIndex.value++;
            if (selectedIndex.value >= level.assetsDictionary.MapAssets.Count)
            {
                selectedIndex.value = level.assetsDictionary.MapAssets.Count-1;
            }
            SelectElementAt((int)selectedIndex.value);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedIndex.value--;
            if (selectedIndex.value < 0)
            {
                selectedIndex.value = 0;
            }
            SelectElementAt((int)selectedIndex.value);
        }
    }


}
