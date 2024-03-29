using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    //scoring
    public float coinScoreValue = 10;
    public float timeScoreValue = 1;
    public float PlayerScore = 0;
    //Coins
    [SerializeField] private int numberToReachForOneUp;
    public int PlayerCoinsNumber = 0;

    public void  PlayerTookCoin() {
        PlayerScore += coinScoreValue;
        PlayerCoinsNumber++;
        if (PlayerCoinsNumber % numberToReachForOneUp == 0) {
            thisPawn.life++;
			GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.OneUp);
		}
	}

    private void FixedUpdate() {
        PlayerScore += timeScoreValue * Time.fixedDeltaTime;
    }
}