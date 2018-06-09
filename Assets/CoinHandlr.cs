using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandlr : MonoBehaviour {

    public GameEvent CoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CoinCollected.Raise();
            Destroy(gameObject);
        }
    }
}
