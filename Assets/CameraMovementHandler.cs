using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementHandler : MonoBehaviour
{


    public TransformVariable playerRef;
    Vector3 originalPos;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;
        targetPos = originalPos;
      //  targetPos.x = playerRef.value.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.value && playerRef.value.position.x > targetPos.x)
        {
            targetPos.x = playerRef.value.position.x;

        }
        transform.position = Vector3.Lerp(transform.position, targetPos, .5f);
    }
}
