using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : AMapElement, ISelectable
{
    string data = "";

    public GameObject Mashrom;
    public GameObject Coins;
    public int invintorySize = 5;
    public GameObject Star;
    public GameObject UI;

    public GameObject CoinSpawn;
    public PowerUpHandler mashromSpawn;
    public PowerUpHandler starSpawn;

    public GameEvent coinCollected;

    public BoolVariable IsEditMode;
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
        if (data != "2")
        {
            invintorySize = 1;
        }
        if (IsEditMode.value)
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (transform.position.y > collision.transform.position.y)
            {
                if (transform.position.x - Mathf.Abs(collision.transform.position.x) >= -.4)
                {
                    if (--invintorySize >= 0)
                        switch (data)
                        {
                            case "1":
                                Spawn(mashromSpawn);
                                break;
                            case "2":
                                Throw(CoinSpawn);
                                break;
                            case "3":
                                Spawn(starSpawn);
                                break;
                            default:
                                break;
                        }
                }
            }
        }
    }

    private void Throw(GameObject SpawnObject)
    {
        var asset = GameObject.Instantiate<GameObject>(SpawnObject);
        asset.transform.position = transform.position + Vector3.up;
        StartCoroutine(ThrowAnim(asset));
    }

    private IEnumerator ThrowAnim(GameObject asset)
    {
        var rb = asset.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        Destroy(asset);
        coinCollected.Raise();
    }

    private void Spawn(PowerUpHandler spawnable)
    {
        var asset = GameObject.Instantiate<GameObject>(spawnable.gameObject);
        asset.transform.position = transform.position + Vector3.up;
    }
}
