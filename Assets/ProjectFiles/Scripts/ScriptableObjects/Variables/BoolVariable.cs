﻿using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/BoolVariable", order = 3)]
public class BoolVariable: ScriptableObject
{

    public bool value;

    public void SetValue(bool val)
    {
        value = val;
    }

}
