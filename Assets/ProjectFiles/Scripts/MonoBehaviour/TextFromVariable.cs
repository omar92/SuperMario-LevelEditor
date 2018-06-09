using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFromVariable : MonoBehaviour
{

    public Text text;
    
    // Use this for initialization

    public void PopulateWith(FloatVariable fv)
    {
        text.text = fv.value.ToString();
    }
 
}
