using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : AMapElement, ISelectable
{
    string data = "";

    public GameObject Mashrom;
    public GameObject Coins;
    public GameObject Star;
    public GameObject UI;

    public override string GetData()
    {
        return data;
    }

    public override void SetData(string data)
    {
        this.data = data;
        SetState(data);
    }

    private void SetState(string data)
    {

        Mashrom.SetActive(false);
        Coins.SetActive(false);
        Star.SetActive(false);

        if (data == "1")
        {
            Mashrom.SetActive(true);
        }
        else if (data == "2")
        {
            Coins.SetActive(true);
        }
        else if (data == "3")
        {
            Star.SetActive(true);
        }
    }


    private void Awake()
    {
        OnUI(false);
    }

    void Start()
    {
        SetState(data);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUI(bool IsOn)
    {
        UI.SetActive(IsOn);
    }
}
