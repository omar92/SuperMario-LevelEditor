using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectsHandler : MonoBehaviour
{

    public TransformVariable camera;
    public Vector2 badding;

    // Update is called once per frame
    Transform checkChild;
    Vector3 screenPoint;
    Camera mainCamera;
    int childCount;
    void Update()
    {
        if (!mainCamera)
        {
            mainCamera = camera.value.GetComponent<Camera>();
        }
        else
        {
            childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                checkChild = transform.GetChild(i);
                screenPoint = mainCamera.WorldToViewportPoint(checkChild.position);
                if (/*screenPoint.z > 0 &&*/ screenPoint.x > -badding.x && screenPoint.x < 1 + badding.x && screenPoint.y > -badding.y && screenPoint.y < 1 + badding.y)
                {
                    checkChild.gameObject.SetActive(true);
                }
                else
                {
                    checkChild.gameObject.SetActive(false);
                }
            }
        }
    }
}
