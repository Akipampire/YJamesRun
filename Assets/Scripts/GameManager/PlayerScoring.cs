using UnityEngine;
using System;

public class PlayerScoring : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    //scoring
    public int coinScoreValue = 10;
    private float PlayerScore = 0f;
    [SerializeField] private int numberToReachForOneUp;
    public int PlayerCoinsNumber = 0;

    public void  PlayerTookCoin() {
        thisPawn.score += coinScoreValue;
        PlayerCoinsNumber++;
        if (PlayerCoinsNumber % numberToReachForOneUp == 0) {
            thisPawn.life++;
			GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.OneUp);
		}
	}

    private void FixedUpdate() {
        PlayerScore = thisPawn.transform.position.z;
        if (PlayerScore % 10f == 0)
            thisPawn.score += 1;
    }
}