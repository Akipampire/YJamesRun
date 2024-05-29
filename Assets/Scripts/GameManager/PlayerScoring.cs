using UnityEngine;
using System;

public class PlayerScoring : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    //scoring
    public int coinScoreValue = 5;
    [SerializeField] private int numberToReachForOneUp;
    public int PlayerCoinsNumber = 0;

    public void  PlayerTookCoin() {
        PlayerCoinsNumber++;
        if (PlayerCoinsNumber % numberToReachForOneUp == 0) {
            thisPawn.life++;
			GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.OneUp);
		}
	}

    private void FixedUpdate() {
        thisPawn.score = (int)thisPawn.transform.position.z + coinScoreValue*PlayerCoinsNumber; //le score vaut la distance parcourue depuis le début + 10 par pièces récupérées
    }
}