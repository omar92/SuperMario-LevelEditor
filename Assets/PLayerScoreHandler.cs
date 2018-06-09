using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerScoreHandler : MonoBehaviour {

    public FloatVariable playerScore;
    public GameEvent ScoreUpdated;

    public void OnCoinCollected()
    {
        playerScore.value += 10;
        ScoreUpdated.Raise();
    }

    public void OnEnemyKilled()
    {
        playerScore.value += 100;
        ScoreUpdated.Raise();
    }
}