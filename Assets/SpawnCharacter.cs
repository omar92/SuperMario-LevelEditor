using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject PlayerPref;
    public TransformVariable mapRef;
    public TransformVariable playerRef;
    public BoolVariable isEditMode;

    public void Spawn()
    {
        if (!isEditMode.value)
        {
            var Player = GameObject.Instantiate<GameObject>(PlayerPref);
            Player.transform.position = transform.position;
            Player.name = "player";
            playerRef.value = Player.transform;
          //  Player.transform.parent = mapRef.value.transform;
        }
    }
}
