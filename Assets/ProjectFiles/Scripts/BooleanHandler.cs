using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BooleanHandler : MonoBehaviour
{

    public BoolVariable boolV;

    public UnityEvent IsTrue;
    public UnityEvent IsFalse;

    void Start()
    {
        if (boolV.value)
        {
            IsTrue.Invoke();
        }
        else
        {
            IsFalse.Invoke();
        }
    }


}
