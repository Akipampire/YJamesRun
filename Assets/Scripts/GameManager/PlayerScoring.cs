using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    //scoring
    public float coinScoreValue = 10;
    public float timeScoreValue = 1;
    public float PlayerScore = 0;
    //Coins
    public int PlayerCoinsNumber = 0;

    public void  PlayerTookCoin() {
        PlayerScore += coinScoreValue;
        PlayerCoinsNumber++;
    }

    private void FixedUpdate() {
        PlayerScore += timeScoreValue * Time.fixedDeltaTime;
    }
}
